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

        public Mine()
        {
            Position = new Vector2(200, 200);
        }

        public void Enter()
        {
            Debug.WriteLine("Worker is trying to enter the mine");
            semaphore.WaitOne();
            Debug.WriteLine("Worker has entered the mine");
        }

        public void Exit()
        {
            semaphore.Release();
            Debug.WriteLine("Worker has exited the mine");
        }

        public override void LoadContent(ContentManager contentManager)
        {
            Sprite = contentManager.Load<Texture2D>("GoldMine_Active");
        }

        public override void Update(GameTime gameTime)
        {
            //
        }
    }
}
