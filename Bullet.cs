using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace MyGame
{
    public class Bullet : Sprite, ICloneable
    {
        static Vector2 Speed { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }
        public Sprite parent;
        public bool IsHidden
        {
            get
            {
                return Position.X > MainGame.Width || Position.X < 0 || Position.Y > MainGame.Height || Position.Y < 0;
            }
        }

        public Bullet(Texture2D texture, Vector2 position, Vector2 speed) : base(texture)
        {
            Position = position;
            Speed = speed;
        }

        public void Update()
        {
            Position += Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
