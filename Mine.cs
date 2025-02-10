using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Threading;

namespace warcraft_4
{
    internal class Mine : GameObject
    {
        private Semaphore semaphore = new Semaphore(3, 3);

        private int workerCount = 0;
        private readonly object workerCountLock = new object();

        public Mine()
        {
            Position = new Vector2(200, 200);
        }

        public void Enter()
        {
            Debug.WriteLine("Worker is trying to enter the mine");
            semaphore.WaitOne();
            lock(workerCountLock)
            {
                workerCount++;
            }
            Debug.WriteLine("Worker has entered the mine");
        }

        public void Exit()
        {
            semaphore.Release();
            lock (workerCountLock)
            {
                workerCount--;
                Debug.WriteLine("Worker has exited the mine");
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
            Sprite = contentManager.Load<Texture2D>("GoldMine_Active");
        }

        public override void Update(GameTime gameTime)
        {
            //
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(GameWorld.spriteFont, $"{workerCount}/3", new Vector2(position.X-8, position.Y-100), Color.White);
        }
    }
}
