/// @file
/// @brief File contains all in-game score handling.
/// 
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

using System;

namespace Zetris
{
	/// <summary>
	/// Handles all in-game scoring.
	/// </summary>
	class ScoreSystem
	{
		/// <summary>
		/// Score per line complete.
		/// </summary>
		private const uint _LINE_SCORE_AMMOUNT = 20;
		/// <summary>
		/// Score per piece place.
		/// </summary>
		private const uint _PER_PIECE_SCORE_AMMOUNT = 12;
		/// <summary>
		/// Max combo level.
		/// </summary>
		private const byte _MAX_COMBO_LVL = 5;
		/// <summary>
		/// Running score.
		/// </summary>
		private uint _score;
		/// <summary>
		/// Score display Update.
		/// </summary>
		private Action<uint> _scoreDisplayUpdate;

		/// <summary>
		/// Running combo level.
		/// </summary>
		private byte _comboLvl;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="scoreDisplayUpdate">In game active score display</param>
		public ScoreSystem(Action<uint> scoreDisplayUpdate)
		{
			_score = 0;
			_comboLvl = 1;
			_scoreDisplayUpdate = scoreDisplayUpdate;
		}

		/// <summary>
		/// Increments score using per line score.
		/// </summary>
		/// <param name="linesCompleted"></param>
		public void LineComplete(byte linesCompleted)
		{
			// Get final score with combo level
			_score += (uint)(Math.Pow(
				_LINE_SCORE_AMMOUNT, linesCompleted) * _comboLvl);

			// Increment combo level
			if (_comboLvl < _MAX_COMBO_LVL) _comboLvl++;
			
			// Update score
			UpdateScoreDisplay();
		}

		public void PiecePlaced()
		{
			_score += _PER_PIECE_SCORE_AMMOUNT;
			// Reset combo level.
			_comboLvl = 1;
			// Update score
			UpdateScoreDisplay();
		}

		/// <summary>
		/// Save Score.
		/// </summary>
		/// <param name="playerName">Player name</param>
		public void SaveScore(string playerName)
		{
			SaveFile save = new SaveFile();

			string finalName = "";

			// Save first 3 letters
			for (int i = 0; i < 3 && i < playerName.Length; i++)
				finalName += playerName[i];

			// Fill missing letters
			for (int j = finalName.Length; j < 3; j++)
				finalName += '-';

			// Save score
			save.SaveScore(finalName.ToUpper(), _score);
		}

		/// <summary>
		/// Update visual
		/// </summary>
		private void UpdateScoreDisplay()
		{
			_scoreDisplayUpdate(_score);
		}
	}
}