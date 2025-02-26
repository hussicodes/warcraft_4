﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace warcraft_4
{
    public class Base : GameObject
    {
        //public int Gold { get; private set; } = 0;
        private int Gold = 20;
        private Mutex goldMutex = new Mutex();
        public List<Worker> Workers { get; private set; } = new List<Worker>();
        private Mine mine;
        private Texture2D baseSprite;

        


        public Base(Mine mine)
        {
            this.mine = mine;
            Position = new Vector2(900,365 );
        }

        public Worker SummonWorker()
        {
            Random rand = new Random();
            if (Gold >= 20)
            {
                Gold -= 20;
                Worker newWorker = new Worker(new Vector2(Position.X + rand.Next(-50, 50), Position.Y + 180 + rand.Next(-50, 50)), mine, this);
                Workers.Add(newWorker);
                Console.WriteLine("Worker summoned!");
                return newWorker;
            }
            else
            {
                Console.WriteLine("Not enough gold to summon a worker!");
                return null;
            }
        }

        public void ReceiveGold(int amount)
        {
         
            goldMutex.WaitOne();
            try
            {
                Thread.Sleep(100);
                Gold += amount;
                Console.WriteLine($"Base received {amount} gold. Total: {Gold}");
            }
            finally
            {
                goldMutex.ReleaseMutex();
            }
       
        }

        public override void LoadContent(ContentManager contentManager)
        {
            baseSprite = contentManager.Load<Texture2D>("Castle_Blue");
            Sprite = baseSprite;
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(GameWorld.spriteFont, $"{Gold} Goldbars", new Vector2(position.X - 8, position.Y - 120), Color.White);
        }

    }
}
