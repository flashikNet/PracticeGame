using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy : AnimatedSprite
    {
        static float epsilon = 1f;
        Vector2 previousPosition;
        public int HealthPoint { get; private set; } = 5;
        public Enemy(Texture2D texture, int rows, int column) : base(texture,rows, column)
        {
        }

        public Enemy(Texture2D texture, Vector2 position, int rows, int columns) : base(texture, position, rows, columns)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MoveToPlayer(gameTime);
        }

        public void TakeDamage(int damage)
        {
            HealthPoint-= damage;
            if (HealthPoint == 1) color = Color.DarkRed;
        }

        public void MoveToPlayer(GameTime gameTime)
        {
            var target = MainGame.Player.Position;
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Position.X - target.X < -epsilon)
            {
                Position.X += Speed * time;
                rotateAngle = 0;
            }
            if (Position.X - target.X > epsilon)
            {
                Position.X -= Speed * time;
                rotateAngle = (float)MathF.PI;
            }
            if (Position.Y - target.Y < -epsilon)
            {
                Position.Y += Speed * time;
                rotateAngle = (float)MathF.PI / 2;
            }
            if(Position.Y - target.Y > epsilon)
            {
                Position.Y -= Speed * time;
                rotateAngle = -(float)MathF.PI / 2;
            }
            if (Position.X - previousPosition.X > epsilon)
            {
                rotateAngle = 0;
            }
            else if (Position.X - previousPosition.X < -epsilon)
            {
                rotateAngle = MathF.PI;
            }
            if (Position.Y - previousPosition.Y < -epsilon)
            {
                rotateAngle = -MathF.PI / 2;
            }
            else if (Position.Y - previousPosition.Y > epsilon)
            {
                rotateAngle = MathF.PI / 2;
            }
            previousPosition = Position;
        }
    }
}
