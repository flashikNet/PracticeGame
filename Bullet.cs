using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace MyGame
{
    public class Bullet : Sprite, ICloneable
    {

        public Sprite Parent;
        public Vector2 Direction;
        public bool IsDone
        {
            get
            {
                return Position.X > MainGame.Width || Position.X < 0 || Position.Y > MainGame.Height || Position.Y < 0;
            }
        }

        public Bullet(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed * Direction;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
