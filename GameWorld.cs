using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace warcraft_4
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static SpriteFont spriteFont;

        private List<GameObject> gameObjects = new List<GameObject>();

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            var mine = new Mine();
            gameObjects.Add(mine);

            var worker = new Workers(new Vector2(300,300));
            gameObjects.Add(worker);
            worker.WalkTo(new Vector2(500,500));

            //Simulating 10 workers trying to enter the mine - Oliver
            //This is just for testing, in the future the real worker code should be used when it is done - Oliver
            List<Thread> threads = new List<Thread>();
            for(int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(() =>
                {
                    mine.Enter();
                    Thread.Sleep(1000); //Simulate worker mining..
                    mine.Exit();
                });
                thread.IsBackground = true;
                threads.Add(thread);
            }

            Thread testThread = new Thread(() =>
            {
                foreach(var thread in threads)
                {
                    thread.Start();
                    Thread.Sleep(300); //Sleep 300ms so that not every worker enters the mine at the same time
                }
            });
            testThread.IsBackground = true;
            testThread.Start();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteFont = Content.Load<SpriteFont>("font");

            foreach (var gameobject in gameObjects)
            {
                gameobject.LoadContent(Content);
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach(var gameobject in gameObjects)
            {
                gameobject.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach(var gameobject in gameObjects)
            {
                gameobject.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
