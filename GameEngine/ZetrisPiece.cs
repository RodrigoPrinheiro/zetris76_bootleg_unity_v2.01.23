using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
	class ZetrisPiece
	{
		public Vector2 position { get; }
		public uint rotation { get; }
		public string shape { get; }

		public ZetrisPiece(string shape)
		{
			this.shape = shape;
		}
	}
}
