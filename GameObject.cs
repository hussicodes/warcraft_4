using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace warcraft_4
{
    public abstract class GameObject
    {



        private Texture2D sprite;
        protected Vector2 position;
        public Vector2 Position { get => position; set => position = value; }
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public abstract void LoadContent(ContentManager contentManager);
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //if (Sprite != null)
            //{
            //    spriteBatch.Draw(Sprite, Position, Color.White);
            //}
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, new Vector2(Sprite.Width / 2, Sprite.Height / 2), 1, SpriteEffects.None, 0);
        }
        public abstract void Update(GameTime gameTime);

    }
}
