using System;

namespace GameEngine
{
	public class GameLoop
	{
		private const long _MS_PER_UPDATE = 1000L / 60L;

		private bool _gameOver;
		public Action ObjectUpdate	{ get; set; }
		public Action WorldUpdate	{ get; set; }
		public Action ScreenUpdate	{ get; set; }
		public Action GameOver		{ get; set; }

		public static InputSystem Input { get; private set; }

		public GameLoop(int width, int height)
		{
			_gameOver = false;
			Input = new InputSystem();

			GameOver += Input.TerminateSystem;

			Console.CursorVisible = false;
			Console.SetWindowSize(width, height);
			Console.SetBufferSize(width, height);
		}

		public void Start()
		{
			long previous = DateTime.Now.Ticks;
			long lag = 0L;
			long current;
			long elapsed;

			while (!_gameOver)
			{
				current = DateTime.Now.Ticks;
				elapsed = current - previous;
				previous = current;
				lag += elapsed;

				while(lag >= _MS_PER_UPDATE)
				{
					Update();
					lag -= _MS_PER_UPDATE;
				}

				Render();
			}

			GameOver.Invoke();
		}

		private void Update()
		{
			ObjectUpdate.Invoke();
			WorldUpdate.Invoke();
		}

		private void Render()
		{
			ScreenUpdate.Invoke();
		}

		public void TriggerGameOver()
		{
			_gameOver = true;
		}
	}
}
