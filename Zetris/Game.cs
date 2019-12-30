using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;

namespace Zetris
{
    class Game
    {
        private int _screenHeight;
        private int _screenWidth;

        // Game UI class
        private IMenu _gameInterface;
        
        // Engine classes
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
            _gameInterface = new ZetrisInterface(_screenWidth, _screenHeight);

            // Initialize gameObjects
            playField = new ZetrisBoard();

            // Add gameObjects to the loop
            _gameLoop.AddGameObject(playField);
        }

        /// <summary>
        /// Runs the game
        /// </summary>
        public void Start()
        {
            _gameLoop.Start();
        }
    }
}
