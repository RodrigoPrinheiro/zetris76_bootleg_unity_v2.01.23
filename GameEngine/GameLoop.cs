using System;
using System.Threading;

namespace GameEngine
{
	public class GameLoop
	{
		private const long _MS_PER_UPDATE = 1000L / 60L;

		private ScreenBuffer<char> _drawBuffer;
		private bool _gameOver;
		private Action objectUpdate;
		private Action<ScreenBuffer<char>> screenUpdate;
		public Action GameOver { get; private set; }
		public static InputSystem Input { get; private set; }

		public GameLoop(int width, int height)
		{
			_gameOver = false;
			Input = new InputSystem();
			_drawBuffer = new ScreenBuffer<char>(width, height);

			//GameOver += Input.TerminateSystem;
			GameOver += TriggerGameOver;
		}

		/// <summary>
		/// Simple Game Object add, puts the object's update and render
		/// to the respective delegate.
		/// </summary>
		/// <param name="obj"> IGameObject to add</param>
		public void AddGameObject(IGameObject obj) 
		{
			objectUpdate += obj.Update;
			screenUpdate += obj.Render;
			
			if (obj.Childs != null)
				foreach (IGameObject child in obj.Childs) 
				{
					objectUpdate += child.Update;
					screenUpdate += child.Render;
				}
		}

		public void Start()
		{
			while (!_gameOver)
			{
				int timeToWait;
				long start = DateTime.Now.Ticks;
				Update();
				Render();

				timeToWait = (int)(start / 10000 + _MS_PER_UPDATE
					- DateTime.Now.Ticks / 10000);
				timeToWait = timeToWait > 0 ? timeToWait : 0;
				Thread.Sleep(timeToWait);
			}
		}

		private void Update()
		{
			objectUpdate?.Invoke();
		}

		private void Render()
		{
			screenUpdate?.Invoke(_drawBuffer);
			_drawBuffer.DrawScreen();
		}

		private void TriggerGameOver()
		{
			_gameOver = true;
		}
	}
}
