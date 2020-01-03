/// @file
/// @brief FIle contains the InputSystem class to use with the game engine.
///
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

using System;
using System.Threading;
using System.Collections.Concurrent;

namespace GameEngine
{
	/// <summary>
	/// InputSystem creates a new thread the gather the inputs from the user
	/// in parallel with the main thread where the game is running.
	/// </summary>
	public class InputSystem
	{
		/// <summary>
		/// Collection for the keys pressed by the user thread-safe.
		/// </summary>
		public BlockingCollection<ConsoleKey> PressedKeys { get; private set; }
		/// <summary>
		/// Thread running the key function.
		/// </summary>
		private Thread _keyProducer;

		/// <summary>
		/// Gets the current key being pressed
		/// </summary>
		/// <param name="key"> Out parameter for the current key</param>
		/// <returns> 
		/// Boolean true or false depending on if it could get a key
		/// </returns>
		public bool CurrentKey(out ConsoleKey key)
		{
			return PressedKeys.TryTake(out key);
		}

		/// <summary>
		/// InputSystem Constructor, starts the thread with the ReadKeys method
		/// </summary>
		public InputSystem()
		{
			PressedKeys = new BlockingCollection<ConsoleKey>();
			_keyProducer = new Thread(ReadKeys);
			_keyProducer.Start();
		}

		/// <summary>
		/// Reads the keys being pressed and adds them to the 
		/// thread safe collection
		/// </summary>
		private void ReadKeys()
		{
			do
			{
				ConsoleKey ck;
				ck = Console.ReadKey(true).Key;
				PressedKeys.Add(ck);
			} while (true);
		}

		/// <summary>
		/// Terminates the input thread.
		/// </summary>
		public void TerminateSystem()
		{
			_keyProducer.Join();
		}
	}
}
