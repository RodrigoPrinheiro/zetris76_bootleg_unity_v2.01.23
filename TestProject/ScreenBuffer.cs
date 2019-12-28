using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    public class ScreenBuffer
    {
        //initiate important variables
        private char[,] screenBufferArray; //main buffer array
        private string screenBuffer; //buffer as string (used when drawing)
        private int roomWidth, roomHeight;

        public ScreenBuffer(int width, int height)
        {
            roomHeight = height;
            roomWidth = width;
            screenBufferArray = new char[roomWidth, roomHeight];
        }

        public char this[int x, int y]
        {
            set
            {
                screenBufferArray[x, y] = value;
            }
        }
        public void DrawScreen()
        {
            screenBuffer = "";
            //iterate through buffer, adding each value to screenBuffer
            for (int iy = 0; iy < roomHeight - 1; iy++)
            {
                for (int ix = 0; ix < roomWidth; ix++)
                {
                    screenBuffer += screenBufferArray[ix, iy];
                }
            }
            //set cursor position to top left and draw the string
            Console.SetCursorPosition(0, 0);
            Console.Write(screenBuffer);
            screenBufferArray = new char[roomWidth, roomHeight];
        }
    }
}
