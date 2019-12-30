using System;
using System.Collections.Generic;
using System.Text;

namespace Zetris
{
    /// <summary>
    /// Zetris piece class, Zetromino since the original and oficial name for
    /// a tetris piece is Tetromino.
    /// </summary>
    class Zetromino
    {
        public string Shape { get; }
        public int Index { get; }
        public Zetromino(string shape, int i) 
        {
            Shape = shape;
            Index = i;
        }
    }
}
