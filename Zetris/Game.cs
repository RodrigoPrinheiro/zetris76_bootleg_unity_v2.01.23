/// @file
/// @brief File contains Game class, this class is where the gameloop and
/// game objects get used together.

using System;
using System.Text;
using GameEngine;

namespace Zetris
{
	/// <summary>
	/// Builds and runs the recreation of Tetris
	/// </summary>
    class Game
    {
		/// <summary>
		/// Screen height to be used in the buffer.
		/// </summary>
        private int _screenHeight;
		/// <summary>
		/// Screen width to be used in the buffer.
		/// </summary>
        private int _screenWidth;
		/// <summary>
		/// Player name for the current player.
		/// </summary>
		private string _playerName;

        // Game UI class
        /// <summary>
		/// Game menu instance.
		/// </summary>
		private IMenu _mainMenu;
        
        // Engine classes
		/// <summary>
		/// Game loop instance to run Tetris.
		/// </summary>
        private GameLoop _gameLoop;

        // Game Objects
		/// <summary>
		/// Zetris board game Object.
		/// </summary>
        private ZetrisBoard playField;

		/// <summary>
		/// Constructor for the game, sets the console buffer and size to the
		/// desirable sizes.
		/// </summary>
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

		/// <summary>
		/// Initializes the game with the player name and starts the loop.
		/// </summary>
		private void InitializeGame()
		{
			_playerName = GetPlayerName();
			_gameLoop = new GameLoop(_screenWidth, _screenHeight);

			// Initialize gameObjects
			playField = new ZetrisBoard(_playerName);

			// Add gameObjects to the loop
			_gameLoop.AddGameObject(playField);
			playField.SetGameOverTrigger(_gameLoop.GameOver);

			_gameLoop.Start();
		}

		/// <summary>
		/// Only visible method, Runs the game from the main menu.
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

		/// <summary>
		/// Creates a player name
		/// </summary>
		/// <returns> String with the player name</returns>
		private string GetPlayerName()
		{
			string name;

			Console.Clear();
			Console.WriteLine("Please Input your name \n(ENTER when done) ");
			name = Console.ReadLine();

			return name;
		}
	}
}
