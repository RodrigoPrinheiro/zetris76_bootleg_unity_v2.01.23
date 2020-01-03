/// @file
/// @brief File contains a PreviewBox which is used to write a simple box to the
/// buffer with a designated user content.
/// 
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

using System.Collections.Generic;
using GameEngine;

namespace Zetris
{
    /// <summary>
    /// Anchors a box with a string content to another game object
    /// </summary>
    class PreviewBox : IGameObject
    {
        /// <summary>
        /// Anchored position
        /// </summary>
        private Vector2 _anchor;
        /// <summary>
        /// Header text of the box
        /// </summary>
        private string _topText;
        /// <summary>
        /// Character to be used as box borders
        /// </summary>
        private char _borders;
        /// <summary>
        /// Box content
        /// </summary>
        private string _content;

        /// <summary>
        /// List of child objects
        /// </summary>
        public List<IGameObject> Childs { get; }
        /// <summary>
        /// Property to get and set the content of the box.
        /// </summary>
        public string Content 
        { 
            get => _content;
            set 
            {
                _content = value.PadRight(16);
            }
        }

        /// <summary>
        /// Preview box constructor, it needs an anchor, header and border.
        /// </summary>
        /// <param name="anchor"> Anchor position X, Y of the box</param>
        /// <param name="header">
        /// Header text to be written on top of the box
        /// </param>
        /// <param name="border"> Border character to be used as borders</param>
        public PreviewBox(Vector2 anchor, string header, char border) 
        {
            _anchor = anchor;
            _topText = header;
            _borders = border;
        }

        /// <summary>
        /// Render method for the box
        /// </summary>
        /// <param name="buffer"> Currently in use buffer</param>
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

        /// <summary>
        /// Update of the box
        /// </summary>
        public void Update()
        {

        }
    }
}
