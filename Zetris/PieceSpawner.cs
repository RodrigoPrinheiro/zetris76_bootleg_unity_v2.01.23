using System;
using System.Collections.Generic;
using System.Text;

namespace Zetris
{
    class PieceSpawner
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
        private Random _rnd;

        public PieceSpawner() 
        {
            _rnd = new Random();
        }

        /// <summary>
        /// Creates a new Zetronimo
        /// </summary>
        /// <returns> Returns the new Zetronimo piece</returns>
        public Zetromino Spawn() 
        {
            return new Zetromino(_PIECES[_rnd.Next() % 7]);
        }
    }
}
