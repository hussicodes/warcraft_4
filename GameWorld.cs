using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using SharpDX.Direct2D1;
using SharpDX.XInput;

namespace warcraft_4
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Tileset tileset;
        private Map map;
        Texture2D texture;
        Texture2D Tileset;
        Rectangle rectangle;

        public static SpriteFont spriteFont;

        private List<GameObject> gameObjects = new List<GameObject>();

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

            int tileSize = 32;
            int screenWidth = graphics.PreferredBackBufferWidth;
            int screenHeight = graphics.PreferredBackBufferHeight;

            graphics.PreferredBackBufferWidth = 1300;  // Sets width of program screen, NOT game screen! Adjust in Map too or you get Nintendo DS effect
            graphics.PreferredBackBufferHeight = 600; // Sets height of program screen, NOT game screen! Adjust in Map too or you get Nintendo DS effect
            graphics.ApplyChanges();
            int tilesX = screenWidth / tileSize;
            int tilesY = screenHeight / tileSize;

            tileset = new Tileset(Content.Load<Texture2D>("tileset"));
            map = new Map(tilesX, tileset);
            

            var mine = new Mine();
            gameObjects.Add(mine);

            var @base = new Base(mine);


            const int workerAmount = 10;
            var workers = new Worker[workerAmount];

            var rand = new Random();
            var spread = 70;

            for (int i = 0; i < workerAmount; i++)
            {
                workers[i] = new Worker(new Vector2(640 + rand.Next(-spread, spread), 360 + rand.Next(-spread, spread)), mine, @base);
                gameObjects.Add(workers[i]);
            }

            Thread startWalk = new Thread(() =>
            {
                for(int i = 0; i < workers.Length; i++)
                {
                    workers[i].WalkTo(new Vector2(200, 200));
                    Thread.Sleep(700);
                }
            });

            startWalk.IsBackground = true;

            startWalk.Start();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);  I needed to use this version for it to work

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

            map.Draw(spriteBatch);

            foreach(var gameobject in gameObjects)
            {
                gameobject.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
