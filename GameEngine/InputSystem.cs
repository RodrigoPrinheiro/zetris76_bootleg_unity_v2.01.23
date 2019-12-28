using System;
using System.Threading;
using System.Collections.Concurrent;

namespace GameEngine
{
	public class InputSystem
	{
		public BlockingCollection<ConsoleKey> PressedKeys { get; private set; }
		private Thread _keyProducer;

		public bool CurrentKey(out ConsoleKey key)
		{
			return PressedKeys.TryTake(out key);
		}

		public InputSystem()
		{
			PressedKeys = new BlockingCollection<ConsoleKey>();
			_keyProducer = new Thread(ReadKeys);
			_keyProducer.Start();
		}
		private void ReadKeys()
		{
			do
			{
				ConsoleKey ck;
				ck = Console.ReadKey(true).Key;
				PressedKeys.Add(ck);
			} while (true);
		}

		public void TerminateSystem()
		{
			_keyProducer.Join();
		}
	}
}
