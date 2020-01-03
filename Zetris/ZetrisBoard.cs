/// @file
/// @brief Tetris playable area class.
/// 
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

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
        // Speeds up the game every 10 pieces placed
        private const int _SPEED_UP = 10;
        // Maximum speed
        private const int _MAX_SPEED = 5;
        #endregion

        /// <summary>
        /// Board is the play area, a byte array with values ranging from 0 to 9
        /// corresponding to each element of the _BOARD constant.
        /// </summary>
        private byte[] board;
        /// <summary>
        /// Variable used to store game dimensions, x and y size of the board.
        /// </summary>
        private Vector2 _gameDim;

        // Game Variables
        /// <summary>
        /// PieceSpawner type variable used to spawn new Zetrominos
        /// </summary>
        private PieceSpawner _spawner;
        /// <summary>
        /// Score system class to store the score and save it after the player
        /// causes a Game Over.
        /// </summary>
		private ScoreSystem _scoreSystem;
        /// <summary>
        /// List of size 4 to store the lines the player created in that update
        /// </summary>
        private List<int> _lines;
        /// <summary>
        /// Boolean to store if the game is over or not
        /// </summary>
        private bool _gameOver;

        // Current Piece tracking variables
        /// <summary>
        /// Zetromino type member to store the currently controlled piece.
        /// </summary>
        private Zetromino _currentPiece;
        /// <summary>
        /// Counter for when The current controlled piece should be pushed down.
        /// </summary>
        private short _gravityCounter;
        /// <summary>
        /// Boolean to control if the current piece has rotated or not in this
        /// update.
        /// </summary>
        private bool _hasRotated;
        /// <summary>
        /// Currently controlled piece rotation value.
        /// </summary>
        private int _pieceRotation;
        /// <summary>
        /// Current piece Y position.
        /// </summary>
        private int _currentY;
        /// <summary>
        /// Current piece X position.
        /// </summary>
        private int _currentX;
        /// <summary>
        /// Control value for how fast the current piece is being pushed down
        /// </summary>
        private int _pieceAcceleration;

        // Line checking variables
        /// <summary>
        /// Timer for how long do the lines stay on screen until they are cleared.
        /// </summary>
        private int _lineTimer;

        // Other variables
        /// <summary>
        /// Spawn location of the pieces.
        /// </summary>
        private Vector2 _spawn;
        /// <summary>
        /// PreviewBox with the next selected piece.
        /// </summary>
        private PreviewBox _nextPiecePreview;
        /// <summary>
        /// ScoreDisplay with the current player score.
        /// </summary>
		private ScoreDisplay _scoreDisplay;
        /// <summary>
        /// Counter to track how many pieces has the player placed.
        /// </summary>
        private int _pieceCounter;
        /// <summary>
        /// Current player name
        /// </summary>
		private string _playerName;
        /// <summary>
        /// Delegate for when a piece is placed
        /// </summary>
        private Action piecePlaced;
        /// <summary>
        /// Delegate for when game over is triggered.
        /// </summary>
        private Action triggerGameOver;
        /// <summary>
        /// Delegate for when a line is complete.
        /// </summary>
        private Action<byte> lineComplete;
        
        /// <summary>
        /// List of child game objects
        /// </summary>
        public List<IGameObject> Childs { get; }

        /// <summary>
        /// Zetris board constructor
        /// </summary>
        /// <param name="playerName">Current player name</param>
        public ZetrisBoard(string playerName)
        {
			_playerName = playerName;

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
            _pieceCounter = 0;

			_scoreDisplay = new ScoreDisplay(new Vector2(_gameDim.x, _gameDim.y - 7));
			_scoreSystem = new ScoreSystem(_scoreDisplay.UpdateScore);

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
			Childs.Add(_scoreDisplay);
            _nextPiecePreview.Content = _spawner.NextShape;

			lineComplete += _scoreSystem.LineComplete;
			piecePlaced += _scoreSystem.PiecePlaced;
		}

        /// <summary>
        /// Update method from IGameObject
        /// </summary>
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
                OnLineComplete((byte)_lines.Count);
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
                    UpdateSpeed();
                    CheckLine();
                    PickNewPiece();
                    _gameOver = !PieceFits
                        (_currentPiece, _pieceRotation, _currentX, _currentY);
                }
                // Reset gravity counter
                _gravityCounter = 0;
            }

            // Check gameover
            if (_gameOver)
                OnGameOver();
        }

        /// <summary>
        /// Render Method from IGameObject
        /// </summary>
        /// <param name="buffer">Screen buffer to be written to</param>
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

        /// <summary>
        /// Sets outside of the class the delegate for a game over.
        /// </summary>
        /// <param name="gameOverTrigger"> Game Over Delegate</param>
		public void SetGameOverTrigger(Action gameOverTrigger)
		{
			triggerGameOver += gameOverTrigger;
		}

        /// <summary>
        /// Checks for lines in the current play.
        /// </summary>
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

        /// <summary>
        /// Picks a new piece to be used as the current piece and resets values
        /// </summary>
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
            OnPiecePlaced();
        }

        /// <summary>
        /// Updates game speed
        /// </summary>
        private void UpdateSpeed() 
        {
            _pieceCounter++;
            if (_pieceCounter % _SPEED_UP == 0)
                if (_pieceAcceleration > _MAX_SPEED) _pieceAcceleration--;
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

        /// <summary>
        /// When a piece is placed this action is called.
        /// </summary>
        private void OnPiecePlaced() 
        {
            piecePlaced?.Invoke();
        }

        /// <summary>
        /// When a line is complete this action is called
        /// </summary>
        /// <param name="lines"> Number of lines done in a row</param>
        private void OnLineComplete(byte lines) 
        {
            lineComplete?.Invoke(lines);
        }

        /// <summary>
        /// When the game ends this action is called.
        /// </summary>
		private void OnGameOver()
		{
			_scoreSystem.SaveScore(_playerName);
			triggerGameOver?.Invoke();
		}
    }
}
