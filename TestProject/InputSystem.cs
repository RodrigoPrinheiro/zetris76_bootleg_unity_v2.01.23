using System.Threading;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System;

namespace TestProject
{
    class InputSystem
    {
        BlockingCollection<ConsoleKey> pressedKeys;
        public bool CurrentKey(out ConsoleKey key)
        {
            return pressedKeys.TryTake(out key);
        }

        Thread keyProducer;
        public InputSystem()
        {
            pressedKeys = new BlockingCollection<ConsoleKey>();
            keyProducer = new Thread(ReadKeys);
            keyProducer.Start();
        }
        private void ReadKeys()
        {
            ConsoleKey ck;
            do
            {
                ck = Console.ReadKey(true).Key;
                pressedKeys.Add(ck);
            } while (ck != ConsoleKey.Escape);
        }

        public void TerminateSystem()
        {
            keyProducer.Join();
        }
    }
}
