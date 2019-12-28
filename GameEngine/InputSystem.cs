using System;
using System.Threading;
using System.Collections.Concurrent;

namespace GameEngine
{
	class InputSystem
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
			ConsoleKey ck;
			ck = Console.ReadKey(true).Key;
			PressedKeys.Add(ck);
		}

		public void TerminateSystem()
		{
			_keyProducer.Join();
		}
	}
}
