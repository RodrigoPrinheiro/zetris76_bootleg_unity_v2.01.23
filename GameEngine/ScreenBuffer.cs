/// @file
/// @brief This file contains the ScreenBuffer class used to write to the console
/// terminal
/// 
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

using System;

namespace GameEngine
{
    /// <summary>
    /// Screen buffer used to write to the screen elements of type T
    /// </summary>
    /// <typeparam name="T"> 
    /// The type of the pixels to write to the screen
    /// </typeparam>
	public class ScreenBuffer<T>
	{
        //initiate important variables
        /// <summary>
        /// Current buffer being rendered
        /// </summary>
        private T[,] _currentBuffer;
        /// <summary>
        /// Next buffer to be rendered
        /// </summary>
        private T[,] _nextBuffer;
        /// <summary>
        /// X Dimension of the buffer 
        /// </summary>
        public int XDim { get; }
        /// <summary>
        /// Y Dimension of the buffer 
        /// </summary>
        public int YDim { get; }

        /// <summary>
        /// ScreenBuffer constructor, initializes the buffer arrays with the
        /// proper dimensions
        /// </summary>
        /// <param name="width"> X dimension of the buffer</param>
        /// <param name="height"> Y dimension of the buffer</param>
        public ScreenBuffer(int width, int height)
        {
            YDim = height;
            XDim = width;
            _currentBuffer = new T[XDim, YDim];
            _nextBuffer = new T[XDim, YDim];
        }

        /// <summary>
        /// Indexer used to write to the next buffer and read from the current
        /// </summary>
        /// <param name="x"> X to look at</param>
        /// <param name="y"> Y to look at</param>
        /// <returns> Variable of generic type of the buffer</returns>
        public T this[int x, int y]
        {
            set
            {
                _nextBuffer[x, y] = value;
            }
            get => _currentBuffer[x, y];
        }

        /// <summary>
        /// Set's an array of generic types T to the buffer, starting at index
        /// x,y and finishing at T[] length.
        /// </summary>
        /// <param name="array"> array to write to the buffer</param>
        /// <param name="x"> X position of the array</param>
        /// <param name="y"> Y position of the array</param>
        public void SetArray(T[] array, int x, int y) 
        {
            for (int i = 0; i < array.Length; i++)
            {
                this[x + i, y] = array[i];
            }
        }

        /// <summary>
        /// Swaps the buffers and draws the next frame.
        /// </summary>
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

        /// <summary>
        /// Swaps the buffers
        /// </summary>
        private void Swap() 
        {
            T[,] aux = _currentBuffer;
            _currentBuffer = _nextBuffer;
            _nextBuffer = aux;

        }

        /// <summary>
        /// Clears the next buffer.
        /// </summary>
        private void Clear()
        {
            Array.Clear(_nextBuffer, 0, _nextBuffer.Length);
        }
    }
}
