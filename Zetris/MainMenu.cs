using System;
using GameEngine;
using System.Collections.Generic;
using System.Text;

namespace Zetris
{
	class MainMenu : IMenu
	{
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

		private const string _START_TXT =
			"\t\t ------------------------\n" +
			"\t\t | press ENTER to start |\n" +
			"\t\t ------------------------\n";
		private const string _EXIT_TXT =
			"\t\t ------------------------\n" +
			"\t\t |  press ESC to exit   |\n" +
			"\t\t ------------------------\n";

		private const string _CONTROLS =
			"\t\t     → : Move Right\n" +
			"\t\t     ← : Move Left\n" +
			"\t\t     ↓ : Fast Drop";

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

		private string GetTopScores()
		{
			SaveFile save = new SaveFile();
			byte i = 0;
			string finalString = "\t\t\tTOP SCORES\n";

			foreach(PlayerScore s in save.GetSavedScores())
			{
				string[] splitScores = s.ToString('\t').Split('\t');
				finalString += "\t\t     "+ splitScores[0] + '\t';
				finalString += splitScores[1].PadLeft(6, '0');
				finalString += "\n";
				i++;
			}

			while (i < 10)
			{
				finalString += "\t\t     ---" + '\t';
				finalString += "".PadLeft(6, '-') + "\n";
				i++;
			}

			return finalString;
		}
	}
}
