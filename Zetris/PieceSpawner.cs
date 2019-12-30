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
        private int _currentPiece;
        public string NextShape { get; private set; }

        public PieceSpawner() 
        {
            _rnd = new Random();
            _currentPiece = _rnd.Next() % 7;
            NextShape = _PIECES[_currentPiece];
        }

        /// <summary>
        /// Creates a new Zetronimo
        /// </summary>
        /// <returns> Returns the new Zetronimo piece</returns>
        public Zetromino GetNewPiece() 
        {
            Zetromino newPiece = new Zetromino
                (NextShape, _currentPiece);
            
            // Pick the next piece
            _currentPiece = _rnd.Next() % 7;
            NextShape = _PIECES[_currentPiece];
            return newPiece;
        }
    }
}
