using System;

namespace GameEngine
{
	public struct PlayerScore : IComparable<PlayerScore>
	{
		public string PlayerName { get; }
		public uint Score { get; }
		
		public PlayerScore(string name, uint score)
		{
			PlayerName = name;
			Score = score;
		}

		public int CompareTo(PlayerScore other) => other.Score.CompareTo(Score);



		public string ToString(char divisorChar) => 
			PlayerName + divisorChar + Score.ToString();
	}
}
