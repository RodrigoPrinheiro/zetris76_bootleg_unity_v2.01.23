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
        private IMenu _mainMenu;
        
        // Engine classes
        private GameLoop _gameLoop;
        private ScreenBuffer<char> _buffer;

        // Game Objects
        private ZetrisBoard playField;

        public Game()
        {
            // Screen size
            
            _screenWidth = 80;
            _screenHeight = 40;
            Console.CursorVisible = false;
            Console.SetWindowSize(_screenWidth, _screenHeight);
			Console.SetBufferSize(_screenWidth, _screenHeight);
			Console.OutputEncoding = Encoding.UTF8;

			_mainMenu = new MainMenu();            
        }

		private void InitializeGame()
		{
			_buffer = new ScreenBuffer<char>(_screenWidth, _screenHeight);
			_gameLoop = new GameLoop(_screenWidth, _screenHeight, _buffer);

			// Initialize gameObjects
			playField = new ZetrisBoard();

			// Add gameObjects to the loop
			_gameLoop.AddGameObject(playField);
			playField.SetGameOverTrigger(_gameLoop.GameOver);

			_gameLoop.Start();
		}

		/// <summary>
		/// Runs the game
		/// </summary>
		public void Start()
        {
			ConsoleKey userKey = ConsoleKey.A;

			while (userKey != ConsoleKey.Escape)
			{
				_mainMenu.ShowMenu();

				userKey = Console.ReadKey().Key;
				if (userKey == ConsoleKey.Enter)
					InitializeGame();

				Console.Clear();
			}
        }
    }
}
