using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace warcraft_4
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseState currentMouseState;
        private MouseState previousMouseState;
        private Base @base;

        public static SpriteFont spriteFont;

        private List<GameObject> gameObjects = new List<GameObject>();
        //private List<Worker> workers = new List<Worker>();
        private Worker[] workers;

        private Worker selectedWorker = null;

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

            @base = new Base(mine);
            gameObjects.Add(@base);

            workers = new Worker[0];


            //const int workerAmount = 10;
            //var workers = new Worker[workerAmount];

            //var rand = new Random();
            //var spread = 70;

            //for (int i = 0; i < workerAmount; i++)
            //{
            //    workers[i] = new Worker(new Vector2(640 + rand.Next(-spread, spread), 360 + rand.Next(-spread, spread)), mine, @base);
            //    gameObjects.Add(workers[i]);
            //}

            //Thread startWalk = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        foreach (var worker in workers)
            //        {
            //            worker?.WalkTo(new Vector2(200, 200));
            //            Thread.Sleep(700);
            //        }
            //        Thread.Sleep(100); // For ikke at loope unødvendigt hurtigt
            //    }
            //});

            //startWalk.IsBackground = true;

            //startWalk.Start();

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

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (previousMouseState.LeftButton == ButtonState.Released &&
                currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

                if (@base != null && new Rectangle((int)@base.Position.X - @base.Sprite.Width / 2,
                                  (int)@base.Position.Y - @base.Sprite.Height / 2,
                                  @base.Sprite.Width,
                                  @base.Sprite.Height).Contains(mousePosition))
                {
                    Worker newWorker = @base.SummonWorker();
                    if (newWorker != null)
                    {
                        newWorker.LoadContent(Content);
                        newWorker.WalkTo(new Vector2(200, 200));

                        // Tilføj til gameObjects
                        gameObjects.Add(newWorker);

                        // Udvid workers-arrayet dynamisk
                        var newWorkers = new Worker[workers.Length + 1];
                        for (int i = 0; i < workers.Length; i++)
                        {
                            newWorkers[i] = workers[i];
                        }
                        newWorkers[newWorkers.Length - 1] = newWorker;
                        workers = newWorkers;
                    }
                }
            }

            // TODO: Add your update logic here


            foreach (var gameobject in gameObjects)
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
