using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
	public class ScreenBuffer<T>
	{
        //initiate important variables
        private T[,] _currentBuffer;
        private T[,] _nextBuffer;
        public int XDim { get; }
        public int YDim { get; }

        public ScreenBuffer(int width, int height)
        {
            YDim = height;
            XDim = width;
            _currentBuffer = new T[XDim, YDim];
            _nextBuffer = new T[XDim, YDim];
        }

        public T this[int x, int y]
        {
            set
            {
                _nextBuffer[x, y] = value;
            }
            get => _currentBuffer[x, y];
        }
        public void DrawScreen()
        {
            Swap();
            Console.SetCursorPosition(0, 0);
            for (int iy = 0; iy < YDim - 1; iy++)
            {
                for (int ix = 0; ix < XDim; ix++)
                {
                    Console.Write(_currentBuffer[ix, iy]);
                }
                Console.WriteLine();
            }
            Clear();
        }

        private void Swap() 
        {
            T[,] aux = _currentBuffer;
            _currentBuffer = _nextBuffer;
            _nextBuffer = aux;

        }

        private void Clear()
        {
            Array.Clear(_nextBuffer, 0, _nextBuffer.Length);
        }
    }
}
