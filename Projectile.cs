using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace MyGame
{
    public class Projectile : Sprite, ICloneable
    {

        public Sprite Parent;
        public Vector2 Direction;
        private static float lifeSpan = 500f;
        private float timeToDie = lifeSpan;
        public int Speed { get; init; } = 200;
        public int Damage { get; init; } = 1;
        public bool IsDone
        {
            get
            {
                return timeToDie <= 0;
            }
        }

        public Projectile(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed * Direction;
            timeToDie -= 1;
        }

        public void Destroy()
        {
            timeToDie = 0;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
            
        }
    }
}
