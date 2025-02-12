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
    internal class Workers : GameObject
    {
        private Texture2D[] idleTexture;
        private int counter;
        private int activeFrame;
        private Vector2 walkTo;
        private float speed = 5.0f;
        private Mine mine;
        private int gold = 0;

        Thread myThreads;

        public Workers(Vector2 startPos, Mine mine)
        {
            myThreads = new Thread(Update2);
            myThreads.IsBackground = true;
            position = startPos;
            walkTo = position;
            this.mine = mine;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            idleTexture = new Texture2D[3];
            idleTexture[0] = contentManager.Load<Texture2D>("Pawn_Blue1");
            idleTexture[1] = contentManager.Load<Texture2D>("Pawn_Blue2");
            idleTexture[2] = contentManager.Load<Texture2D>("Pawn_Blue3");

            myThreads.Start();

        }

        public override void Update(GameTime gameTime)
        {
        }

        public void Update2(object data)
        {
            while (true)
            {
                Thread.Sleep(16);
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

                if (walkTo != position)
                {
                    Vector2 direction = walkTo - position;
                    if (direction.Length() > speed)
                    {
                        direction.Normalize();
                        position += direction * speed;
                    }
                    else
                    {
                        position = walkTo;
                    }
                }

                float distanceToMine = Vector2.Distance(position, mine.Position);
                if(gold == 0 && distanceToMine < 20.0f)
                {
                    mine.Enter();

                    Thread.Sleep(2000); //Work work..
                    gold += 20;

                    mine.Exit();

                    WalkTo(new Vector2(640, 360)); //Walk back to base

                }
            }
        }

        private void EnterMine()
        {
            
        }

        public void WalkTo(Vector2 walkTo)
        {
            this.walkTo = walkTo;
        }
    }
}
