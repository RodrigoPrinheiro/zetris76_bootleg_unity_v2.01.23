using System;
using System.IO;
using System.Collections.Generic;

namespace GameEngine
{
	public class SaveFile
	{
		private const byte _MAX_SAVED_SCORES	= 10;
		private const string _SAVE_DIR_NAME		= "ZetrisGame";
		private const string _SCORE_FILE_NAME	= "ZetrisScores.sav";
		private const char _CHAR_DIVISOR		= '\t';

		private readonly string _dirPath;

		public SaveFile()
		{
			_dirPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
				_SAVE_DIR_NAME);

			if (!SaveDirExists())
				CreateSaveDir();
		}

		public void SaveScore(string playerName, uint score)
		{
			string fullScorePath = Path.Combine(_dirPath, _SCORE_FILE_NAME);
			string finalScoreTxt = default;
			List<PlayerScore> scores = GetSavedScores();

			scores.Add(new PlayerScore(playerName, score));
			scores.Sort();

			for (int i = 0; i < _MAX_SAVED_SCORES && i < scores.Count; i++)
				finalScoreTxt += scores[i].ToString(_CHAR_DIVISOR) + '\n';

			File.WriteAllText(fullScorePath, finalScoreTxt);
		}

		public List<PlayerScore> GetSavedScores()
		{
			string fullScorePath = Path.Combine(_dirPath, _SCORE_FILE_NAME);
			List<PlayerScore> scores = new List<PlayerScore>();

			if (!File.Exists(fullScorePath))
				File.Create(fullScorePath);
			else 
				using (StreamReader ScoreSR = new StreamReader(fullScorePath))
				{
					string line;
					while ((line = ScoreSR.ReadLine()) != null)
					{
						string[] scoreInfo = line.Split(_CHAR_DIVISOR);
						scores.Add(
							new PlayerScore(
								scoreInfo[0], 
								uint.Parse(scoreInfo[1])));
					}
					ScoreSR.Close();
				}

			return scores;
		}

		private bool SaveDirExists() =>
			Directory.Exists(_dirPath);

		private void CreateSaveDir()
		{
			Directory.CreateDirectory(_dirPath);
		}
	}
}