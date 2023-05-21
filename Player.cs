using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player : AnimatedSprite
    {
        public int HealthPoint { get; set; } = 300;
        public Bullet Bullet;
        private static Stopwatch timer = new Stopwatch();
        public int UseRate { get; set; } = 200;
        public Vector2 Offset { get; set; }
        public Player(Texture2D texture, int rows, int columns, int period, Bullet bullet) :base(texture, rows, columns, period)
        {
            this.Bullet = bullet;
            timer.Start();
        }

        public override void Move(GameTime gameTime)
        {
            if (Input == null) throw new Exception("No Controller to Player");
            base.Move(gameTime);

            //Position = Vector2.Clamp(Position, Origin, new Vector2(MainGame.Width - Width / 2, MainGame.Height - Height / 2));

            Shoot();
        }

        private void Shoot()
        {
            var kstate = Keyboard.GetState();
            var time = timer.ElapsedMilliseconds;
            if (time > UseRate)
            {
                if (kstate.IsKeyDown(Input.ShootUp))
                {
                    AddBullet(new Vector2(0, -1), Rotate(Math.PI/2));
                    RotateAngle = (float)-Math.PI / 2;
                    timer.Restart();
                }
                else if (kstate.IsKeyDown(Input.ShootDown))
                {
                    AddBullet(new Vector2(0, 1), Rotate(-Math.PI/2));
                    RotateAngle = (float)Math.PI / 2;
                    timer.Restart();
                }
                else if (kstate.IsKeyDown(Input.ShootRight))
                {
                    AddBullet(new Vector2(1, 0), Offset);
                    RotateAngle = 0;
                    timer.Restart();
                }
                else if (kstate.IsKeyDown(Input.ShootLeft))
                {
                    AddBullet(new Vector2(-1, 0), Rotate(Math.PI));
                    RotateAngle = (float)Math.PI;
                    timer.Restart();
                }
            }
        }

        private void AddBullet(Vector2 direction, Vector2 offset)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Position = Position + offset;
            bullet.Parent = this;
            bullet.Direction = direction;
            MainGame.Sprites.Add(bullet);
        }

        private Vector2 Rotate(double angle)
        {
            var x = Offset.X;
            var y = Offset.Y;
            var cos = (float)Math.Cos(angle);
            var sin = (float)Math.Sin(angle);
            return new Vector2(x * cos + y * sin, y * cos - x * sin);
        }
    }
}
