/// @file
/// @brief File contains a Zetromino piece information structure.

namespace Zetris
{
    /// <summary>
    /// Zetris piece class, Zetromino since the original and oficial name for
    /// a tetris piece is Tetromino.
    /// </summary>
    class Zetromino
    {
        /// <summary>
        /// Shape of the piece.
        /// </summary>
        public string Shape { get; }
        /// <summary>
        /// Index of the piece in the PieceSpawner const array.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// Constructor for the piece, takes a shape and an index.
        /// </summary>
        /// <param name="shape"> String shape of this Zetromino</param>
        /// <param name="i"> Int index of this shape</param>
        public Zetromino(string shape, int i) 
        {
            Shape = shape;
            Index = i;
        }
    }
}
