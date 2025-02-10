using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace warcraft_4
{
    internal class Mine : GameObject
    {
        private Semaphore semaphore = new Semaphore(3, 3);
        
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
            //
        }

        public override void Update(GameTime gameTime)
        {
            //
        }
    }
}
