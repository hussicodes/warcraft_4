using Microsoft.Xna.Framework;
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

        Thread myThreads;

        public Workers()
        {
            myThreads = new Thread(Update2);
            myThreads.IsBackground = true;
            

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
            //counter++;

            //if (counter > 29)
            //{
            //    counter = 0;
            //    activeFrame++;

            //    if (activeFrame > idleTexture.Length - 1)
            //    {
            //        activeFrame = 0;
            //    }
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(idleTexture[activeFrame], new Rectangle(56, 56, 56, 56), Color.White);
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
            }
        }
    }
}
