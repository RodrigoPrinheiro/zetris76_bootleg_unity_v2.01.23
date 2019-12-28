using System;

namespace GameEngine
{
	class GameLoop
	{
		private const long _MS_PER_UPDATE = 1000L / 30L;
		private const int _SCREEN_WIDTH = 30;
		private const int _SCREEN_HEIGHT = 80;

		private bool _gameOver;
		public Action ObjectUpdate	{ get; set; }
		public Action WorldUpdate	{ get; set; }
		public Action ScreenUpdate	{ get; set; }
		public Action GameOver		{ get; set; }

		public static InputSystem Input { get; private set; }

		public GameLoop()
		{
			_gameOver = false;
			Input = new InputSystem();

			GameOver += Input.TerminateSystem;

			Console.CursorVisible = false;
			Console.SetWindowSize(_SCREEN_WIDTH, _SCREEN_HEIGHT);
			Console.SetBufferSize(_SCREEN_WIDTH, _SCREEN_HEIGHT);
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
