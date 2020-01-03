using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using GameEngine;

namespace TestProject
{
    class Program
    {
        private Random rnd;
        private readonly string[] pieces;
        private const int gameWidth = 12;
        private const int gameHeight = 18;
        private byte[] game;
        private ScreenBuffer<char> buffer;
        private const int screenHeight = 30, screenWidth = 80;
        private static InputSystem inputs;

        // Rotates a piece with a given rotation r;
        private int RotatePiece(int px, int py, int r)
        {
            switch (r % 4)
            {
                // 0 degrees
                case 0: return py * 4 + px;
                // 90 degrees
                case 1: return 12 + py - (px * 4);
                // 180 degrees
                case 2: return 15 - (py * 4) - px;
                // 270 degrees
                case 3: return 3 - py + (px * 4);
            }
            return 0;
        }

        bool PieceFits(int nTetrisPiece, int rotation, int posX, int posY)
        {
            // Number 4 because pieces cannot be bigger than a 4x4
            for (int px = 0; px < 4; px++)
            {
                for (int py = 0; py < 4; py++)
                {
                    // Index into piece
                    int pi = RotatePiece(px, py, rotation);

                    // Get game pos of index
                    int gi = (posY + py) * gameWidth + (posX + px);

                    if (posX + px >= 0 && posX + px < gameWidth)
                        if (posY + py >= 0 && posY + py < gameHeight)
                        {
                            if (pieces[nTetrisPiece][pi].Equals('X') && game[gi] != 0)
                                return false;
                        }
                }
            }

            return true;
        }

        private Program()
        {
            // Create screen buffer
            buffer = new ScreenBuffer<char>(screenWidth, screenHeight);

            // Create Input system
            inputs = new InputSystem();

            // Initialize random
            rnd = new Random();

            // Create pieces
            pieces = new string[]
            {"..X." +
             "..X." +
             "..X." +
             "..X.",

             "..X." +
             ".XX." +
             ".X.." +
             "....",

             ".X.." +
             ".XX." +
             "..X." +
             "....",

             "...." +
             ".XX." +
             ".XX." +
             "....",

             "..X." +
             ".XX." +
             "..X." +
             "....",

             "...." +
             ".XX." +
             ".X.." +
             ".X..",

             "...." +
             ".XX." +
             "..X." +
             "..X."
            };

            // Initialize playing field of the game
            game = new byte[gameWidth * gameHeight];
            for (int x = 0; x < gameWidth; x++)
                for (int y = 0; y < gameHeight; y++)
                    game[y * gameWidth + x] =
                        (x == 0 || x == gameWidth - 1 ||
                        y == gameHeight - 1) ? (byte)9 : (byte)0;
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(screenWidth, screenHeight);
            Console.SetBufferSize(screenWidth, screenHeight);
            Program p = new Program();
            p.Execute();

            inputs.TerminateSystem();
        }

        private void Execute()
        {
            // Tracking variables
            bool gameOver = false;
            string controlString = $"\0ABCDEFG=█";
            char[] arr = controlString.ToCharArray(0, controlString.Length);
            List<int> lines = new List<int>();

            int currentPiece = 1;
            int currentRotation = 0;
            int currentX = gameWidth / 2;
            int currentY = 0;
            int speed = 20;
            int speedCounter = 0;


            bool[] key = new bool[4];
            bool hasRotated = false;
            bool forceDown = false;

            // Gameloop
            while (!gameOver)
            {
                // Game Time
                Thread.Sleep(50);
                speedCounter++;
                forceDown = (speedCounter == speed);

                // Input
                ConsoleKey pressedKey;
                inputs.CurrentKey(out pressedKey);
                for (int i = 0; i < 4; i++)
                {
                    key[i] = pressedKey.Equals((ConsoleKey)"\x27\x25\x28Z"[i]);
                }

                // Logic-------------------------------------------------------

                // Incrementes current piece position
                currentX += (key[0] && PieceFits
                    (currentPiece, currentRotation, currentX + 1, currentY)) ? 1 : 0;
                currentX -= (key[1] && PieceFits
                    (currentPiece, currentRotation, currentX - 1, currentY)) ? 1 : 0;
                currentY += (key[2] && PieceFits
                    (currentPiece, currentRotation, currentX, currentY + 1)) ? 1 : 0;

                // Rotates current Piece
                if (key[3])
                {
                    currentRotation += (!hasRotated && PieceFits
                        (currentPiece, currentRotation + 1, currentX, currentY)) ? 1 : 0;
                    hasRotated = true;
                }
                else
                    hasRotated = false;

                // Forces down a piece
                if (forceDown)
                {
                    if (PieceFits(currentPiece, currentRotation, currentX, currentY + 1))
                        currentY++;
                    else
                    {
                        // Lock piece
                        for (int px = 0; px < 4; px++)
                        {
                            for (int py = 0; py < 4; py++)
                            {
                                // Add the piece to the game board
                                if (pieces[currentPiece][RotatePiece(px, py, currentRotation)].Equals('X'))
                                    game[(currentY + py) * gameWidth + (currentX + px)] =
                                        (byte)(currentPiece + 1);
                            }
                        }
                        // Check lines
                        // Last piece that touched the ground
                        for (int py = 0; py < 4; py++)
                        {
                            if (currentY + py < gameHeight - 1)
                            {
                                bool line = true;
                                // Check for line
                                for (int px = 1; px < gameWidth - 1; px++)
                                    line &= (game[(currentY + py) * gameWidth + px]) != 0;

                                // Create the line (set game tile to 8, char array)
                                if (line)
                                {
                                    for (int px = 1; px < gameWidth - 1; px++)
                                        game[(currentY + py) * gameWidth + px] = 8;

                                    lines.Add(currentY + py);
                                }
                            }
                        }

                        // Choose next Piece
                        currentY = 0;
                        currentX = gameWidth / 2;
                        currentRotation = 0;
                        currentPiece = rnd.Next() % 7;

                        // If Cant fit piece then game over
                        gameOver = !PieceFits(currentPiece, currentRotation, currentX, currentY);
                    }

                    speedCounter = 0;
                }

                // Draw Screen
                for (int x = 0; x < gameWidth; x++)
                    for (int y = 0; y < gameHeight; y++)
                        buffer[x + 2, y + 2] = arr[game[y * gameWidth + x]];

                // Draw Current Piece
                for (int px = 0; px < 4; px++)
                    for (int py = 0; py < 4; py++)
                        if (pieces[currentPiece][RotatePiece(px, py, currentRotation)] == 'X')
                            buffer[(currentX + px + 2), (currentY + py + 2)] = (char)(currentPiece + 65);

                // Draw lines and delete them
                if (lines.Count > 0)
                {
                    buffer.DrawScreen();
                    Thread.Sleep(400);

                    foreach (int i in lines)
                        for (int px = 1; px < gameWidth - 1; px++) 
                        {
                            for (int py = i; py > 0; py--)
                                game[py * gameWidth + px] = game[(py - 1) * gameWidth + px];
                            game[px] = 0;
                        }
                    lines.Clear();
                }

                buffer.DrawScreen();
            }

            Console.Clear();
            Console.WriteLine("GAME OVER");
        }
    }
}
