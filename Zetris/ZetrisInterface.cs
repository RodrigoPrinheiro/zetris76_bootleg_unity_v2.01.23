using System;
using System.Collections.Generic;
using System.Text;

namespace Zetris
{
    class ZetrisInterface : IMenu
    {
        private int screenX, screenY;

        public ZetrisInterface(int screenX, int screenY)
        {
            this.screenX = screenX;
            this.screenY = screenY;
        }

        public void ShowMenu()
        {

        }

        private void ShowScores()
        {

        }
    }
}
