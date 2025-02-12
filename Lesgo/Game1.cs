using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SharpDX.XInput;

namespace Lesgo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;
        private Tileset tileset;
        private Map map;
        
        // below are remnants from an alpha build
        Texture2D texture;
        Texture2D Tileset;
        Rectangle rectangle;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
           

            
            int tileSize = 32; // Here you can change the size of your tiles, make em big or make em small, remember to adjust in map class aswell!

            int screenWidth = graphics.PreferredBackBufferWidth;
            int screenHeight = graphics.PreferredBackBufferHeight;

            graphics.PreferredBackBufferWidth = 1300;  // Sets width of program screen, NOT game screen! Adjust in Map too or you get Nintendo DS effect
            graphics.PreferredBackBufferHeight = 600; // Sets height of program screen, NOT game screen! Adjust in Map too or you get Nintendo DS effect
            graphics.ApplyChanges();
            // Calculate how many tiles fit on the screen
            int tilesX = screenWidth / tileSize;
            int tilesY = screenHeight / tileSize;

            
            tileset = new Tileset(Content.Load<Texture2D>("tileset"));

            // Create the map with the calculated size
            map = new Map(tilesX, tileset);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);


            Texture2D tilesetTexture = Content.Load<Texture2D>("tileset");
            tileset = new Tileset(tilesetTexture);
            tileset.AddTile(224, 16, 16, 16);
            tileset.AddTile(240, 16, 16, 16);
            tileset.AddTile(224, 16, 16, 16);
            tileset.AddTile(240, 16, 16, 16);
            map = new Map(32, tileset);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

       

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            spriteBatch.Begin();

            
            map.Draw(spriteBatch);


            spriteBatch.End();

       

            base.Draw(gameTime);
        }
    }
}