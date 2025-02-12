using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcraft_4
{
    public class Base : GameObject
    {
        public int Gold { get; private set; } = 0;
        public List<Worker> Workers { get; private set; } = new List<Worker>();
        private Mine mine;

        public Base(Mine mine)
        {
            this.mine = mine;
        }

        public Worker SummonWorker()
        {
            Worker newWorker = new Worker(new Vector2(0,0), mine, this);
            Workers.Add(newWorker);
            Console.WriteLine("Worker summoned!");
            return newWorker;
        }

        public void ReceiveGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"Base received {amount} gold. Total: {Gold}");
        }

        public override void LoadContent(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
