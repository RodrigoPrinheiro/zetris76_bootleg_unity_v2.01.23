/// @file
/// @brief File handles the main menu display.
///
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

using System;

namespace Zetris
{
	/// <summary>
	/// Used to display main menu on screen.
	/// </summary>
	class MainMenu : IMenu
	{
		/// <summary>
		/// Game Logo.
		/// </summary>
		private const string _ZETRIS_LOGO = 
			@" ________  _______  _________  ________  ___  ________      " + 
			"\n" +
			@"|\_____  \|\  ___ \|\___   ___\\   __  \|\  \|\   ____\     " + 
			"\n" +
			@" \|___/  /\ \   __/\|___ \  \_\ \  \|\  \ \  \ \  \___|_    " + 
			"\n" +
			@"     /  / /\ \  \_|/__  \ \  \ \ \   _  _\ \  \ \_____  \   " + 
			"\n" +
			@"    /  /_/__\ \  \_|\ \  \ \  \ \ \  \\  \\ \  \|____|\  \  " + 
			"\n" +
			@"   |\________\ \_______\  \ \__\ \ \__\\ _\\ \__\____\_\  \ " + 
			"\n" +
			@"    \|_______|\|_______|   \|__|  \|__|\|__|\|__|\_________\" + 
			"\n";

		/// <summary>
		/// Start Game area text.
		/// </summary>
		private const string _START_TXT =
			"\t\t ------------------------\n" +
			"\t\t | press ENTER to start |\n" +
			"\t\t ------------------------\n";
		/// <summary>
		/// Exit Game area text.
		/// </summary>
		private const string _EXIT_TXT =
			"\t\t ------------------------\n" +
			"\t\t |  press ESC to exit   |\n" +
			"\t\t ------------------------\n";

		/// <summary>
		/// Game controls text.
		/// </summary>
		private const string _CONTROLS =
			"\t\t     → : Move Right\n" +
			"\t\t     ← : Move Left\n" +
			"\t\t     ↓ : Soft Drop";

		/// <summary>
		/// Char to display when names are empty.
		/// </summary>
		private const char _EMPTY_NAME_CHAR = '-';
		/// <summary>
		/// Char to display when scores are empty.
		/// </summary>
		private const char _EMPTY_SCORE_CHAR = '.';

		/// <summary>
		/// Show full main menu.
		/// </summary>
		public void ShowMenu()
		{
			Console.WriteLine(_ZETRIS_LOGO);
			Console.WriteLine(GetTopScores());
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine(_START_TXT);
			Console.BackgroundColor = ConsoleColor.DarkRed;
			Console.WriteLine(_EXIT_TXT);
			Console.BackgroundColor = default;
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(_CONTROLS);
		}

		/// <summary>
		/// Extracts saved scores.
		/// </summary>
		/// <returns>Final string to display with scores and names.</returns>
		private string GetTopScores()
		{
			SaveFile save = new SaveFile();
			byte i = 0;
			string finalString = "\t\t\tTOP SCORES\n";

			// Correctly display all saved scores
			foreach(PlayerScore s in save.GetSavedScores())
			{
				// Split name and score
				string[] splitScores = s.ToString('\t').Split('\t');
				// Add name to the final string
				finalString += "\t\t     "+ splitScores[0] + '\t';
				// Add score to the final string
				finalString += splitScores[1].PadLeft(6, '0');
				// Add new line
				finalString += "\n";

				i++;
			}

			// Fill all blank spaces
			while (i < SaveFile._MAX_SAVED_SCORES)
			{
				// Name section
				finalString += $"\t\t     " +
					$"{_EMPTY_NAME_CHAR}{_EMPTY_NAME_CHAR}{_EMPTY_NAME_CHAR}"
					+ '\t';
				// Score section
				finalString += "".PadLeft(6, _EMPTY_SCORE_CHAR) + "\n";
				i++;
			}

			return finalString;
		}
	}
}
