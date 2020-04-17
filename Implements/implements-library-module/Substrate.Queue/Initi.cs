using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Substrate.Queue
{
    class Initi
    {
        private ConcurrentDictionary<string, ConcurrentQueue<string>> queues = new ConcurrentDictionary<string, ConcurrentQueue<string>>();

        public bool AddQueue(string name)
        {
            ConcurrentQueue<string> newQueue = new ConcurrentQueue<string>();

            return queues.TryAdd(name.ToUpper(), newQueue);
        }

        public bool Add()
        {

        }

    }
}
