using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Sprite : Model
    {
        public Texture2D Texture { get; protected set; }
        public Vector2 Position;
        public Vector2 Origin;
        public float Scale { get; set; } = 1f;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
        public Sprite(Texture2D texture)
        {
            this.Texture = texture;
            Origin = new Vector2(texture.Width/2, texture.Height/2);
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            Position = position;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0, Origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
