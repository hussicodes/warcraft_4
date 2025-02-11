using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcraft_4
{
    public class Base
    {
        public int Gold { get; private set; } = 0;
        public List<Worker> Workers { get; private set; } = new List<Worker>();

        public Worker SummonWorker()
        {
            Worker newWorker = new Worker(this);
            Workers.Add(newWorker);
            Console.WriteLine("Worker summoned!");
            return newWorker;
        }

        public void ReceiveGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"Base received {amount} gold. Total: {Gold}");
        }
    }
}
