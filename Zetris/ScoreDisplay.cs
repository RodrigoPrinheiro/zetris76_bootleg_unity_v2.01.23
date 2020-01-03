using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;

namespace Zetris
{
	class ScoreDisplay : IGameObject
	{
		private const byte _BOX_X_SIZE = 6 + 2;
		private const byte _BOX_Y_SIZE = 3;
		private const char _NUMBER_FILLER = '0';
		private const char _BORDER_FILLER = '+';
		private const string _HEADER = "SCORE";

		private Vector2 _anchor;
		private string _score;
		public List<IGameObject> Childs { get; }

		public ScoreDisplay(Vector2 anchor)
		{
			_anchor = anchor;
			_score = "0";
		}

		public void UpdateScore(uint newScore)
		{
			_score = newScore.ToString();
		}

		public void Render(ScreenBuffer<char> buffer)
		{
			int screenStart = (buffer.XDim / 2 - 5);
			int centeredHeaderX = (_BOX_X_SIZE - _HEADER.Length) / 2;
			string paddedScore = _score.PadLeft(_BOX_X_SIZE - 2, _NUMBER_FILLER);

			// Render header
			for (int x = 0; x < _HEADER.Length; x++)
				buffer[ (x + _anchor.x + screenStart) + (centeredHeaderX), _anchor.y-1] =
					_HEADER[x];

			// Render border
			for (int y = 0; y < _BOX_Y_SIZE; y++)
			{
				for (int x = 0; x < _BOX_X_SIZE; x++)
				{
					if (x == 0 || x == _BOX_X_SIZE - 1 || 
						y == _BOX_Y_SIZE - 1 || y == 0)
						buffer[x + _anchor.x + screenStart,
							y + _anchor.y] = _BORDER_FILLER;
				}
			};

			// Render score
			for (int x = 0; x < _BOX_X_SIZE - 2; x++)
					buffer[1 + x + _anchor.x + screenStart, 1 + _anchor.y] = 
						paddedScore[x];
		}

		public void Update() { }
	}
}