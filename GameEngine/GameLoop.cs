/// @file
/// @brief File contains the main game loop and respective methods.
///
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

using System;
using System.Threading;

namespace GameEngine
{
	public class GameLoop
	{
		/// <summary>
		/// Milliseconds to wait per update.
		/// </summary>
		private const long _MS_PER_UPDATE = 1000L / 60L;

		/// <summary>
		/// Game screen buffer.
		/// </summary>
		private ScreenBuffer<char> _drawBuffer;
		/// <summary>
		/// Game has ended.
		/// </summary>
		private bool _gameOver;
		/// <summary>
		/// Stores all object updates.
		/// </summary>
		private Action objectUpdate;
		/// <summary>
		/// Stores all screen updates.
		/// </summary>
		private Action<ScreenBuffer<char>> screenUpdate;
		/// <summary>
		/// Action to perform on Game Over
		/// </summary>
		public Action GameOver { get; private set; }
		/// <summary>
		/// Input system.
		/// </summary>
		public static InputSystem Input { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="width">Screen Width.</param>
		/// <param name="height">Screen Height</param>
		public GameLoop(int width, int height)
		{
			_gameOver = false;
			Input = new InputSystem();
			_drawBuffer = new ScreenBuffer<char>(width, height);

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

		/// <summary>
		/// Starts game loop.
		/// </summary>
		public void Start()
		{
			while (!_gameOver)
			{
				// Update frame info
				int timeToWait;
				long start = DateTime.Now.Ticks;

				// Update Game
				Update();
				Render();

				// Wait until next frame ( capping frames )
				timeToWait = (int)(start / 10000 + _MS_PER_UPDATE
					- DateTime.Now.Ticks / 10000);
				timeToWait = timeToWait > 0 ? timeToWait : 0;
				Thread.Sleep(timeToWait);
			}
		}

		/// <summary>
		/// Invoke all stored Updates.
		/// </summary>
		private void Update()
		{
			objectUpdate?.Invoke();
		}

		/// <summary>
		/// Invoke all stored Renders.
		/// </summary>
		private void Render()
		{
			screenUpdate?.Invoke(_drawBuffer);
			_drawBuffer.DrawScreen();
		}

		/// <summary>
		/// Activates Game Over.
		/// </summary>
		private void TriggerGameOver()
		{
			_gameOver = true;
		}
	}
}
