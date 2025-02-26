﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace warcraft_4
{
    public class Worker : GameObject
    {
        private Texture2D[] idleTexture;
        private int counter;
        private int activeFrame;
        private Vector2 walkTo;
        private float speed = 25.0f;
        private Mine mine;
        private int gold = 0;
        private Texture2D goldTexture;
        private Base @base;

        Thread workerThread;

        public Worker(Vector2 startPos, Mine mine, Base @base)
        {
            workerThread = new Thread(UpdateWorker);
            workerThread.IsBackground = true;
            position = startPos;
            walkTo = position;
            this.mine = mine;
            this.@base = @base;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            idleTexture = new Texture2D[3];
            idleTexture[0] = contentManager.Load<Texture2D>("Pawn_Blue1");
            idleTexture[1] = contentManager.Load<Texture2D>("Pawn_Blue2");
            idleTexture[2] = contentManager.Load<Texture2D>("Pawn_Blue3");

            goldTexture = contentManager.Load<Texture2D>("G_Idle_(NoShadow)");
            Sprite = idleTexture[0];
            workerThread.Start();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void UpdateWorker(object data)
        {
            while (true)
            {
                Thread.Sleep(16);

                Animate();
                Walk();
                HandleMineCollision();
                HandleBaseCollision();
            }
        }

        public void WalkTo(Vector2 walkTo)
        {
            this.walkTo = walkTo;
        }

        private void HandleMineCollision()
        {
            float distanceToMine = Vector2.Distance(position, mine.Position);
            if (gold == 0 && distanceToMine < 50.0f)
            {
                mine.Enter();

                Thread.Sleep(2000); //Work work..
                gold += 15;

                mine.Exit();

                WalkTo(@base.Position); //Walk back to base
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if(gold > 0)
            {
                spriteBatch.Draw(goldTexture, Position, null, Color.White, 0f, new Vector2(Sprite.Width, Sprite.Height), 1, SpriteEffects.None, 0);
            }
        }

        private void HandleBaseCollision()
        {
            float distanceToBase = Vector2.Distance(position, @base.Position);
            if (gold != 0 && distanceToBase < 50.0f)
            {
                @base.ReceiveGold(gold);
                gold = 0;
                WalkTo(mine.Position);
            }
        }

        private void Walk()
        {
            if (walkTo != position)
            {
                Vector2 direction = walkTo - position;
                if (direction.Length() > speed)
                {
                    direction.Normalize();
                    position += direction * speed;
                    Animate();
                }
                else
                {
                    position = walkTo;
                }
            }
        }

        private void Animate()
        {
            counter++;
            if (counter > 29)
            {
                counter = 0;
                activeFrame++;

                if (activeFrame > idleTexture.Length - 1)
                {
                    activeFrame = 0;
                }

            }

            Sprite = idleTexture[activeFrame];
        }
    }
}
