/// @file
/// @brief File contains the score display area information structure.
/// 
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

using System.Collections.Generic;
using GameEngine;

namespace Zetris
{
	/// <summary>
	/// Score display class.
	/// </summary>
	class ScoreDisplay : IGameObject
	{
		/// <summary>
		/// Box width.
		/// </summary>
		private const byte _BOX_X_SIZE = 6 + 2;
		/// <summary>
		/// Box Height
		/// </summary>
		private const byte _BOX_Y_SIZE = 3;
		/// <summary>
		/// Empty number filler.
		/// </summary>
		private const char _NUMBER_FILLER = '0';
		/// <summary>
		/// Border filler.
		/// </summary>
		private const char _BORDER_FILLER = '+';
		/// <summary>
		/// Header string.
		/// </summary>
		private const string _HEADER = "SCORE";

		/// <summary>
		/// Object screen anchor.
		/// </summary>
		private Vector2 _anchor;
		/// <summary>
		/// Displayed score.
		/// </summary>
		private string _score;

		/// <summary>
		/// Game object childs.
		/// </summary>
		public List<IGameObject> Childs { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="anchor">Screen anchor.</param>
		public ScoreDisplay(Vector2 anchor)
		{
			_anchor = anchor;
			_score = "0";
		}

		/// <summary>
		/// Update score string.
		/// </summary>
		/// <param name="newScore">New score to display.</param>
		public void UpdateScore(uint newScore)
		{
			_score = newScore.ToString();
		}

		/// <summary>
		/// Render Object
		/// </summary>
		/// <param name="buffer">Game buffer.</param>
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