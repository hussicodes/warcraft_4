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
        private Mine mine;
        // private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch; Muddi: Min kode virkede ikke uden at specificere XNA framework.
        private Tileset tileset;
        private Map map;

        Texture2D texture;
        Texture2D Tileset;
        Rectangle rectangle;

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
            int tileSize = 32; // Here you can change the size of your tiles, make em big or make em small, remember to adjust in map class aswell!
            
            int screenWidth = _graphics.PreferredBackBufferWidth;
            int screenHeight = _graphics.PreferredBackBufferHeight;
            
            _graphics.PreferredBackBufferWidth = 1300;  // Sets width of program screen, NOT game screen! Adjust in Map too or you get Nintendo DS effect
            _graphics.PreferredBackBufferHeight = 600; // Sets height of program screen, NOT game screen! Adjust in Map too or you get Nintendo DS effect
            _graphics.ApplyChanges();
            
            // Calculate how many tiles fit on the screen
            int tilesX = screenWidth / tileSize;
            int tilesY = screenHeight / tileSize;
            
            tileset = new Tileset(Content.Load<Texture2D>("tileset"));
            
            // Create the map with the calculated size
            
            map = new Map(tilesX, tileset);

            mine = new Mine();
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

            // spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);  Muddi: Min kode igen havde brug for at speicficere XNA framework

            spriteFont = Content.Load<SpriteFont>("font");

             Texture2D tilesetTexture = Content.Load<Texture2D>("tileset");
             tileset = new Tileset(tilesetTexture);
             tileset.AddTile(224, 16, 16, 16);
             tileset.AddTile(240, 16, 16, 16);
             tileset.AddTile(224, 16, 16, 16);
             tileset.AddTile(240, 16, 16, 16);
             map = new Map(32, tileset);


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
                        //newWorker.WalkTo(new Vector2(200, 200));

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

            if (previousMouseState.LeftButton == ButtonState.Released &&
                currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

                foreach (var worker in workers)
                {
                    Rectangle workerRect = new Rectangle((int)worker.Position.X - worker.Sprite.Width / 2,
                                                       (int)worker.Position.Y - worker.Sprite.Height / 2,
                                                       worker.Sprite.Width,
                                                       worker.Sprite.Height);
                    if (workerRect.Contains(mousePosition))
                    {
                        selectedWorker = worker;
                        break;
                    }
                }

                // Handle base and mine selection after selecting a worker
                if (selectedWorker != null)
                {
                    if (mine != null && new Rectangle((int)mine.Position.X - mine.Sprite.Width / 2,
                                      (int)mine.Position.Y - mine.Sprite.Height / 2,
                                      mine.Sprite.Width,
                                      mine.Sprite.Height).Contains(mousePosition))
                    {
                        selectedWorker.WalkTo(mine.Position);

                        // Deselect worker after moving
                        if (selectedWorker != null)
                        {
                            selectedWorker = null;
                        }
                    }
                }



                foreach (var gameobject in gameObjects)
                {
                    gameobject.Update(gameTime);
                }

                base.Update(gameTime);
            }
         
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            map.Draw(_spriteBatch);

            foreach(var gameobject in gameObjects)
            {
                gameobject.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
