using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;

namespace Zetris
{
    class Game
    {
        #region constants
        private static readonly string[] PIECES = new string[]
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

        // Classic tetris width and height of the game's play field
        private int _gameWidth;
        private int _gameHeight;
        private int _screenHeight;
        private int _screenWidth;

        // Random to pick a new piece
        private Random _rnd;
        // Game UI class
        private IMenu _gameInterface;
        private GameLoop _gameLoop;

        public Game()
        {
            // Screen size
            _gameWidth = 12;
            _gameHeight = 18;
            _screenWidth = 80;
            _screenHeight = 40;

            _gameLoop = new GameLoop(_screenWidth, _screenHeight);
            _rnd = new Random();
            _gameInterface = new ZetrisInterface(_screenWidth, _screenHeight);
        }

        /// <summary>
        /// Runs the game
        /// </summary>
        public void Start()
        {
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
