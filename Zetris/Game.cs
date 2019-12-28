using System;
using System.Collections.Generic;
using System.Text;
// using GameEngine;

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
        private int gameWidth;
        private int gameHeight;
        private int screenHeight;
        private int screenWidth;

        // Random to pick a new piece
        private Random rnd;

        // Game UI class
        IMenu gameInterface;

        // Game Spawner class

        // Other variables

        public Game()
        {
            // Screen size
            gameWidth = 12;
            gameHeight = 18;
            screenWidth = 80;
            screenHeight = 40;

            rnd = new Random();
            gameInterface = new ZetrisInterface(screenWidth, screenHeight);
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
