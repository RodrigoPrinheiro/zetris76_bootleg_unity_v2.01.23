/// @file
/// @brief File handles the save file.
///
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

using System;
using System.IO;
using System.Collections.Generic;

namespace Zetris
{
	/// <summary>
	/// Handles all the score saving and extracting.
	/// </summary>
	public class SaveFile
	{
		/// <summary>
		/// Max scores to save.
		/// </summary>
		public const byte _MAX_SAVED_SCORES	= 10;
		/// <summary>
		/// Game directory name.
		/// </summary>
		private const string _SAVE_DIR_NAME		= "ZetrisGame";
		/// <summary>
		/// File name and extension.
		/// </summary>
		private const string _SCORE_FILE_NAME	= "ZetrisScores.sav";
		/// <summary>
		/// Char to divide name and score with .
		/// </summary>
		private const char _CHAR_DIVISOR		= '\t';

		/// <summary>
		/// Path to directory.
		/// </summary>
		private readonly string _dirPath;

		/// <summary>
		/// Class constructor
		/// </summary>
		public SaveFile()
		{
			// Merge all paths
			_dirPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
				_SAVE_DIR_NAME);

			// If save directory does not exist
			if (!SaveDirExists())
				CreateSaveDir();
		}

		/// <summary>
		/// Save given scores in save file.
		/// </summary>
		/// <param name="playerName">Player name to save</param>
		/// <param name="score">Score to save</param>
		public void SaveScore(string playerName, uint score)
		{
			// Get full path
			string fullScorePath = Path.Combine(_dirPath, _SCORE_FILE_NAME);
			string finalScoreTxt = default;
			// Save extracted scores
			List<PlayerScore> scores = (List<PlayerScore>)GetSavedScores();

			// Add new score to extracted scores
			scores.Add(new PlayerScore(playerName, score));

			// Sort all scores
			scores.Sort();

			// Get top 10 scores to a final text
			for (int i = 0; i < _MAX_SAVED_SCORES && i < scores.Count; i++)
				finalScoreTxt += scores[i].ToString(_CHAR_DIVISOR) + '\n';

			// Write final text on save file
			File.WriteAllText(fullScorePath, finalScoreTxt);
		}

		/// <summary>
		/// Extracts all saved scores.
		/// </summary>
		/// <returns>An collection with all saved scores.</returns>
		public IEnumerable<PlayerScore> GetSavedScores()
		{
			// Get full path
			string fullScorePath = Path.Combine(_dirPath, _SCORE_FILE_NAME);
			List<PlayerScore> scores = new List<PlayerScore>();

			// Check if file exists
			if (!File.Exists(fullScorePath))
				File.Create(fullScorePath);
			else 
				// Open and file
				using (StreamReader ScoreSR = new StreamReader(fullScorePath))
				{
					string line;
					// Read every score line
					while ((line = ScoreSR.ReadLine()) != null)
					{
						// Split scores and names
						string[] scoreInfo = line.Split(_CHAR_DIVISOR);
						// Add score to collection
						scores.Add(
							new PlayerScore(
								scoreInfo[0], 
								uint.Parse(scoreInfo[1])));
					}
					ScoreSR.Close();
				}

			return scores;
		}

		/// <summary>
		/// Check if save file directory exists.
		/// </summary>
		/// <returns>True if exists</returns>
		private bool SaveDirExists() =>
			Directory.Exists(_dirPath);

		/// <summary>
		/// Create game save directory.
		/// </summary>
		private void CreateSaveDir()
		{
			Directory.CreateDirectory(_dirPath);
		}
	}
}