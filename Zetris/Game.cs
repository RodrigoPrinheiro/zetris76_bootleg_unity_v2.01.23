using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;

namespace Zetris
{
    class Game
    {
        #region constants
        private static readonly string[] _PIECES = new string[]
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
        #endregion

        private int _screenHeight;
        private int _screenWidth;

        // Random to pick a new piece
        private Random _rnd;
        // Game UI class
        private IMenu _gameInterface;
        private GameLoop _gameLoop;
        private ScreenBuffer<char> _buffer;

        // Game Objects
        private IGameObject playField;

        public Game()
        {
            // Screen size
            
            _screenWidth = 80;
            _screenHeight = 40;
            Console.CursorVisible = false;
            Console.SetWindowSize(_screenWidth, _screenHeight);
			Console.SetBufferSize(_screenWidth, _screenHeight);

            _buffer = new ScreenBuffer<char>(_screenWidth, _screenHeight);
            _gameLoop = new GameLoop(_screenWidth, _screenHeight, _buffer);
            _rnd = new Random();
            _gameInterface = new ZetrisInterface(_screenWidth, _screenHeight);

            playField = new ZetrisBoard();

            _gameLoop.ScreenUpdate += playField.Render;
        }

        /// <summary>
        /// Runs the game
        /// </summary>
        public void Start()
        {
            _gameLoop.Start();
            // Menu
            // Start Game

                // Inputs
                // Do something with inputs
                
                // Force pieces down
                // Lock pieces that cant move in place
                // Check for lines

                // Pick a new Piece

                // Check for a game over

                // Draw Screen

            // Go back to Menu
        }
    }
}
