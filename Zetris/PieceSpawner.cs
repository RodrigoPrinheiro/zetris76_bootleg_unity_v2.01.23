/// @file
/// @brief File contains PieceSpawner, spawns and manages the next piece to be
/// used as the currently controlled player piece.
/// 
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date

using System;

namespace Zetris
{
    /// <summary>
    /// PieceSpawner class has methods to return a Zetromino on demand.
    /// </summary>
    class PieceSpawner
    {
        #region constants
        /// <summary>
        /// Possible Zetromino's shapes.
        /// </summary>
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
        /// <summary>
        /// Random variable instance to get a random piece.
        /// </summary>
        private Random _rnd;
        /// <summary>
        /// Currently picked NEXT piece index.
        /// </summary>
        private int _currentPiece;
        /// <summary>
        /// Next shape in string format.
        /// </summary>
        public string NextShape { get; private set; }

        /// <summary>
        /// PieceSpawner Constructor, initializes random instance and creates
        /// a piece to be start off.
        /// </summary>
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
