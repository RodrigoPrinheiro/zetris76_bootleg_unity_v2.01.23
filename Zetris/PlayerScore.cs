/// @file
/// @brief File contains the score struct, used to store a player name and
/// a player score. 
/// value
/// 
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

using System;

namespace Zetris
{
	/// <summary>
	/// Struct stores given name and score
	/// </summary>
	public struct PlayerScore : IComparable<PlayerScore>
	{
		/// <summary>
		/// Player name.
		/// </summary>
		public string PlayerName { get; }
		/// <summary>
		/// Player Score.
		/// </summary>
		public uint Score { get; }

		/// <summary>
		/// Constructor assigns name and score.
		/// </summary>
		/// <param name="name">Player name.</param>
		/// <param name="score">Player score.</param>
		public PlayerScore(string name, uint score)
		{
			PlayerName = name;
			Score = score;
		}

		/// <summary>
		/// Compare Scores with other scores.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(PlayerScore other) => other.Score.CompareTo(Score);

		/// <summary>
		/// Get a string containing the player name and score separated by the
		/// given char.
		/// </summary>
		/// <param name="divisorChar"></param>
		/// <returns></returns>
		public string ToString(char divisorChar) => 
			PlayerName + divisorChar + Score.ToString();
	}
}
