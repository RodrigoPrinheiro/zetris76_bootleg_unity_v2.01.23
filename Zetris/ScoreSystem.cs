using System;

namespace Zetris
{
	class ScoreSystem
	{
		private const uint _LINE_SCORE_AMMOUNT = 20;
		private const uint _PER_PIECE_SCORE_AMMOUNT = 12;
		private const byte _MAX_COMBO_LVL = 5;
		private uint _score;
		private Action<uint> _scoreDisplayUpdate;

		private byte _comboLvl;

		public ScoreSystem(Action<uint> scoreDisplayUpdate)
		{
			_score = 0;
			_comboLvl = 1;
			_scoreDisplayUpdate = scoreDisplayUpdate;
		}

		public void LineComplete(byte linesCompleted)
		{
			_score += (uint)(Math.Pow(
				_LINE_SCORE_AMMOUNT, linesCompleted) * _comboLvl);

			if (_comboLvl < _MAX_COMBO_LVL) _comboLvl++;
			
			_scoreDisplayUpdate(_score);
		}

		public void PiecePlaced()
		{
			_score += _PER_PIECE_SCORE_AMMOUNT;
			_comboLvl = 1;
			_scoreDisplayUpdate(_score);
		}

		public void SaveScore(string playerName)
		{
			SaveFile save = new SaveFile();
			save.SaveScore(playerName, _score);
		}
	}
}