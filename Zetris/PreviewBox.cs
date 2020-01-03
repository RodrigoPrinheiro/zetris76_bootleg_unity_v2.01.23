using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;

namespace Zetris
{
    class PreviewBox : IGameObject
    {
        private Vector2 _anchor;
        private string _topText;
        private char _borders;
        private string _content;

        public List<IGameObject> Childs { get; }
        public string Content 
        { 
            get => _content;
            set 
            {
                _content = value.PadRight(16);
            }
        }

        public PreviewBox(Vector2 anchor, string header, char border) 
        {
            _anchor = anchor;
            _topText = header;
            _borders = border;
        }

        public void Render(ScreenBuffer<char> buffer)
        {
            int screenStart = (buffer.XDim / 2 - 5);
            buffer.SetArray(_topText.ToCharArray(), _anchor.x + screenStart,
                _anchor.y - 1);

            // Render border
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    if (x == 0 || x == 5 || y == 5 || y == 0)
                        buffer[x + _anchor.x + screenStart,
                            y + _anchor.y] = _borders;
                }
            }

            // Render Contents
            if (Content != null) 
            {
                for (int y = 1; y < 5; y++)
                {
                    for (int x = 1; x < 5; x++)
                    {
                        buffer[x + _anchor.x + screenStart, y + _anchor.y] = _content
                            [(y - 1) * 4 + (x - 1)];
                    }
                }
            }
        }

        public void Update()
        {

        }
    }
}
