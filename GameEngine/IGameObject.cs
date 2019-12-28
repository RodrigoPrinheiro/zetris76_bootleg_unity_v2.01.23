using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
	public interface IGameObject
	{
		void Update();
		void Render(ScreenBuffer<char> buffer);
	}
}
