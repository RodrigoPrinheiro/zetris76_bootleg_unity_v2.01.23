using System;
using System.Collections.Generic;
using System.Text;

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
		public uint y { get; }
		/// <summary>
		/// Top Screen position.
		/// </summary>
		public uint x { get; }

		/// <summary>
		/// Vector 2 constructor
		/// </summary>
		/// <param name="x">Initial X position.</param>
		/// <param name="y">Initial Y position.</param>
		public Vector2(uint x, uint y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
