using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
	public class GameObject
	{
		public Vector2 position { get; }
		public uint rotation { get; }
		public string shape { get; }

		public GameObject(string shape)
		{
			this.shape = shape;
		}
	}
}
