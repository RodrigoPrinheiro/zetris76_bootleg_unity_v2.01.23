/// @file
/// @brief File contains the Vector2 struct, used to store an x, y coordinates 
/// value
/// 
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

namespace GameEngine
{
	/// <summary>
	/// Struct stores the position on screen
	/// </summary>
	public struct Vector2
	{
		/// <summary>
		/// Right screen position.
		/// </summary>
		public int y { get; }
		/// <summary>
		/// Top Screen position.
		/// </summary>
		public int x { get; }

		/// <summary>
		/// Vector 2 constructor
		/// </summary>
		/// <param name="x">Initial X position.</param>
		/// <param name="y">Initial Y position.</param>
		public Vector2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
