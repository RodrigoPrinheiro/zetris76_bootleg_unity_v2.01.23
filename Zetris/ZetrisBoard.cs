using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;

namespace Zetris
{
    class ZetrisBoard : IGameObject
    {
        private const string _BOARD = "\0ABCDEFG=█";
        private byte[] board;
        private int _gameWidth;
        private int _gameHeight;

        // Game Variables
        private PieceSpawner _spawner;
        private Zetromino _currentPiece;
        private int _currentY;
        private int _currentX;

        public ZetrisBoard() 
        {
            // Classic tetris dimensions
            _gameWidth = 12;
            _gameHeight = 18;

            board = new byte[_gameWidth * _gameHeight];
            for (int x = 0; x < _gameWidth; x++) 
            {
                for (int y = 0; y < _gameHeight; y++) 
                {
                    board[y * _gameWidth + x] = 
                        (x == 0 || x == _gameWidth - 1 || y == _gameHeight - 1)
                        // 9 is the last character of the board string in Game
                        ? (byte)9 : (byte)0;
                }
            }
        }

        public byte this[int i] 
        {
            get => board[i];
            set 
            {
                board[i] = value;
            }
        }

        public void Update() 
        {
        
        }

        public void Render(ScreenBuffer<char> buffer) 
        {
            char[] boardPieces = _BOARD.ToCharArray(0, _BOARD.Length);

            // Add to buffer the whole board with the correct characters
            for (int x = 0; x < _gameWidth; x++)
                for (int y = 0; y < _gameHeight; y++) 
                {
                    buffer[x + buffer.XDim / 2 - 5, y + 10] = 
                        boardPieces[board[y * _gameWidth + x]];
                }
        }
    }
}
