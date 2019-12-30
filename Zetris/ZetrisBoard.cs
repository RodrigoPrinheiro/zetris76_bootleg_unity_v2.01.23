using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;

namespace Zetris
{
    /// <summary>
    /// Zetris board is composed by all the elements of the PLAYABLE game,
    /// this means the current piece that the player can control and all of the
    /// pieces the player as set down on the board.
    /// </summary>
    class ZetrisBoard : IGameObject
    {
        #region constants
        private const string _BOARD = "_ABCDEFG=█";
        // Right Left Down Z
        private const string _CONTROLS = "\x27\x25\x28Z";
        // 16 = 1 second, now do the math
        private const int _LINE_TIMER = 10;
        #endregion

        /// <summary>
        /// Board is the play area, a byte array with values ranging from 0 to 9
        /// corresponding to each element of the _BOARD constant.
        /// </summary>
        private byte[] board;
        private Vector2 _gameDim;

        // Game Variables
        private PieceSpawner _spawner;
        private List<int> _lines;
        private bool _gameOver;

        // Current Piece tracking variables
        private Zetromino _currentPiece;
        private short _gravityCounter;
        private bool _hasRotated;
        private int _pieceRotation;
        private int _currentY;
        private int _currentX;
        private int _pieceAcceleration;

        // Line checking variables
        private int _lineTimer;

        // Other variables
        private Vector2 _spawn;
        private PreviewBox _nextPiecePreview;

        public List<IGameObject> Childs { get; }

        public ZetrisBoard()
        {
            // Classic tetris dimensions
            _gameDim = new Vector2(12, 18);
            // Spawn location
            _spawn = new Vector2((_gameDim.x / 2) - 2, 0);
            // Current piece X and Y position
            _currentX = _spawn.x;
            _currentY = _spawn.y;

            _spawner = new PieceSpawner();
            _currentPiece = _spawner.GetNewPiece();
            _pieceRotation = 0;
            _pieceAcceleration = 16;
            _hasRotated = false;
            _lines = new List<int>(4);

            // Create the board
            board = new byte[_gameDim.x * _gameDim.y];
            for (int x = 0; x < _gameDim.x; x++)
            {
                for (int y = 0; y < _gameDim.y; y++)
                {
                    board[y * _gameDim.x + x] =
                        (x == 0 || x == _gameDim.x - 1 || y == _gameDim.y - 1)
                        // 9 is the last character of the board string in Game
                        ? (byte)9 : (byte)0;
                }
            }

            Childs = new List<IGameObject>();
            _nextPiecePreview = new PreviewBox(new Vector2
                (_gameDim.x, _gameDim.y + 3), "Next Piece", 'o');

            Childs.Add(_nextPiecePreview);
            _nextPiecePreview.Content = _spawner.NextShape;
        }

        public void Update()
        {
            // Update Counters
            _gravityCounter++;
            if (_lines.Count > 0)
                _lineTimer++;
            else
            {
                UpdateCurrentPiece();
                _lineTimer = 0;
            }

            // Clear lines if a bit of time has passed;
            if (_lines.Count > 0 && (_lineTimer >= _LINE_TIMER))
            {
                foreach (int i in _lines)
                    for (int px = 1; px < _gameDim.x - 1; px++)
                    {
                        for (int py = i; py > 0; py--)
                        {
                            // Replace the row bellow with the one above
                            board[py * _gameDim.x + px] =
                                board[(py - 1) * _gameDim.x + px];
                        }
                        board[px] = 0;
                    }

                _lines.Clear();
            }
            // Force down
            else if (_gravityCounter >= _pieceAcceleration)
            {
                // Move current piece down
                if (PieceFits
                    (_currentPiece, _pieceRotation, _currentX, _currentY + 1))
                {
                    _currentY++;
                }
                else
                {
                    LockPiece();
                    CheckLine();
                    PickNewPiece();
                    _gameOver = !PieceFits
                        (_currentPiece, _pieceRotation, _currentX, _currentY);
                }

                // Reset gravity counter
                _gravityCounter = 0;
            }
        }

        public void Render(ScreenBuffer<char> buffer)
        {
            char[] boardPieces = _BOARD.ToCharArray(0, _BOARD.Length);
            int boardStartX = buffer.XDim / 2 - 5;
            int boardStartY = 10;

            // Add to buffer the whole board with the correct characters
            for (int x = 0; x < _gameDim.x; x++)
                for (int y = 0; y < _gameDim.y; y++)
                {
                    buffer[x + boardStartX, y + boardStartY] =
                        boardPieces[board[y * _gameDim.x + x]];
                }

            // Draw Current Piece
            for (int px = 0; px < 4; px++)
                for (int py = 0; py < 4; py++)
                    if (_currentPiece.Shape[RotatePiece(px, py, _pieceRotation)]
                        == 'X')
                    {
                        // Add piece square to the buffer starting at A + index
                        buffer[(_currentX + px + boardStartX),
                            (_currentY + py + boardStartY)] =
                            (char)(_currentPiece.Index + 65);
                    }


        }

        private void CheckLine()
        {
            // Last piece that touched the ground
            for (int py = 0; py < 4; py++)
            {
                if (_currentY + py < _gameDim.y - 1)
                {
                    bool line = true;
                    // Check for line
                    for (int px = 1; px < _gameDim.x - 1; px++)
                        line &= (board[(_currentY + py) * _gameDim.x + px]) != 0;

                    // Create the line (set game tile to 8, char array)
                    if (line)
                    {
                        for (int px = 1; px < _gameDim.x - 1; px++)
                            board[(_currentY + py) * _gameDim.x + px] = 8;

                        _lines.Add(_currentY + py);
                    }
                }
            }
        }

        private void PickNewPiece()
        {
            _currentPiece = _spawner.GetNewPiece();
            _currentX = _spawn.x;
            _currentY = _spawn.y;
            _pieceRotation = 0;
            _nextPiecePreview.Content = _spawner.NextShape;
        }

        /// <summary>
        /// Updates the location of the currently controlled Zetromino 
        /// </summary>
        private void UpdateCurrentPiece()
        {
            bool[] key = new bool[4];
            // Move the current piece with the current input
            ConsoleKey currentKey;
            GameLoop.Input.CurrentKey(out currentKey);
            // For each control in Controls add it to the key press array
            for (int i = 0; i < 4; i++)
            {
                key[i] = currentKey.Equals((ConsoleKey)_CONTROLS[i]);
            }

            // Update Left
            _currentX += (key[0] && PieceFits
                (_currentPiece, _pieceRotation, _currentX + 1, _currentY))
                ? 1 : 0;
            // Update Right
            _currentX -= key[1] && PieceFits
                (_currentPiece, _pieceRotation, _currentX - 1, _currentY)
                ? 1 : 0;
            // Update Down
            _currentY += key[2] && PieceFits
                (_currentPiece, _pieceRotation, _currentX, _currentY + 1)
                ? 1 : 0;
            // Update Rotation
            if (key[3])
            {
                _pieceRotation += (!_hasRotated && PieceFits
                    (_currentPiece, _pieceRotation + 1, _currentX, _currentY))
                    ? 1 : 0;
                _hasRotated = true;
            }
            else
                _hasRotated = false;

        }

        /// <summary>
        /// Adds the current piece to the board
        /// </summary>
        private void LockPiece()
        {
            for (int px = 0; px < 4; px++)
            {
                for (int py = 0; py < 4; py++)
                {
                    // Add the piece to the game board
                    if (_currentPiece.Shape[RotatePiece(px, py, _pieceRotation)]
                        .Equals('X'))
                    {
                        board[(_currentY + py) * _gameDim.x + (_currentX + px)] =
                            (byte)(_currentPiece.Index + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Rotates a piece based on a rotation r
        /// </summary>
        /// <param name="x"> Zetromino shape X</param>
        /// <param name="y"> Zetromino shape Y</param>
        /// <param name="r"> Rotation of the Zetromino piece</param>
        /// <returns> Index in the Zetromino shape string</returns>
        private int RotatePiece(int x, int y, int r)
        {
            switch (r % 4)
            {
                // 0 degrees
                case 0: return y * 4 + x;
                // 90 degrees
                case 1: return 12 + y - (x * 4);
                // 180 degrees
                case 2: return 15 - (y * 4) - x;
                // 270 degrees
                case 3: return 3 - y + (x * 4);
            }
            return 0;
        }

        /// <summary>
        /// "Physics" collision for a Zetromino piece
        /// </summary>
        /// <param name="piece"> Piece to check collision from</param>
        /// <param name="rotation"> Rotation of the piece</param>
        /// <param name="posX"> position X to check</param>
        /// <param name="posY"> position Y to check</param>
        /// <returns> False if the piece can't be in the position,
        /// True if it can</returns>
        bool PieceFits(Zetromino piece, int rotation, int posX, int posY)
        {
            // Number 4 because pieces cannot be bigger than a 4x4
            for (int px = 0; px < 4; px++)
            {
                for (int py = 0; py < 4; py++)
                {
                    // Index into piece
                    int pi = RotatePiece(px, py, rotation);

                    // Get game pos of index
                    int gi = (posY + py) * _gameDim.x + (posX + px);

                    // If its inside the game field
                    if (posX + px >= 0 && posX + px < _gameDim.x)
                        if (posY + py >= 0 && posY + py < _gameDim.y)
                        {
                            // Is the game field empty in the next position
                            if (piece.Shape[pi].Equals('X') && board[gi] != 0)
                                return false;
                        }
                }
            }

            return true;
        }
    }
}
