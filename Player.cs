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
        public int HealthPoint { get; private set; }
        public Projectile Bullet;
        private static Stopwatch timer = new Stopwatch();
        private static Stopwatch damageTimer = new Stopwatch();
        public int UseRate { get; set; } = 200;
        int boost = 3;
        public Vector2 Offset { get; set; }
        Vector2 minPos, maxPos;
        int immmortalTime = 500;
        public int ShiftTime { get; private set; }

        public InputManager Input { get; init; }
        public ProjectileManager ProjectileManager { get; init; }
        public Player(Texture2D texture, int rows, int columns, int period, Projectile bullet, int healthPoint = 100) :base(texture, rows, columns, period)
        {
            this.Bullet = bullet;
            ShiftTime = 10000;
            timer.Start();
            damageTimer.Start();
            HealthPoint = healthPoint;
        }

        public void TakeDamage(int damage)
        {
            if (damageTimer.ElapsedMilliseconds > immmortalTime)
            {
                damageTimer.Restart();
                HealthPoint -= damage;
                color = Color.Multiply(Color.White, 0.5f);
                if (HealthPoint <= 0) MainGame.State = State.Over;
            }
            
        }

        private void SetDamagable()
        {
            if(damageTimer.ElapsedMilliseconds > immmortalTime)
            {
                color = Color.White;
            }
        }
        public void SetBounds(Map map)
        {
            var tileSize = map.TileSize;
            var mapSize = map.MapSize;
            minPos = new((-tileSize.X / 2) + Origin.X, (-tileSize.Y / 2) + Origin.Y);
            maxPos = new(mapSize.X - (tileSize.X / 2) - Origin.X, mapSize.Y - (tileSize.X / 2) - Origin.Y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Move(gameTime);
            Shoot();
            Position = Vector2.Clamp(Position, minPos, maxPos);
            SetDamagable();
        }
        private void Move(GameTime gameTime)
        {
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var isBoosted = false;
            if (Input.IsKeyPressed(Keys.LeftShift) && ShiftTime > 0)
            {
                Speed *= boost;
                ShiftTime -= 16;
                ShiftTime = ShiftTime < 0 ? 0 : ShiftTime;
                isBoosted = true;
            }
            if (Input.IsKeyDown(Keys.A))
                Position.X -= Speed * time;
            if (Input.IsKeyDown(Keys.D))
                Position.X += Speed * time;
            if (Input.IsKeyDown(Keys.W))
                Position.Y -= Speed * time;
            if (Input.IsKeyDown(Keys.S))
                Position.Y += Speed * time;
            if(isBoosted) Speed /= boost;
        }

        private void Shoot()
        {
            var time = timer.ElapsedMilliseconds;
            if (time > UseRate)
            {
                if (Input.IsKeyDown(Keys.Up))
                {
                    AddBullet(new Vector2(0, -1), Offset.Rotate(Math.PI/2));
                    rotateAngle = -MathF.PI / 2;
                    timer.Restart();
                }
                else if (Input.IsKeyDown(Keys.Down))
                {
                    AddBullet(new Vector2(0, 1), Offset.Rotate(-Math.PI/2));
                    rotateAngle = MathF.PI / 2;
                    timer.Restart();
                }
                else if (Input.IsKeyDown(Keys.Right))
                {
                    AddBullet(new Vector2(1, 0), Offset);
                    rotateAngle = 0;
                    timer.Restart();
                }
                else if (Input.IsKeyDown(Keys.Left))
                {
                    AddBullet(new Vector2(-1, 0), Offset.Rotate(Math.PI));
                    rotateAngle = MathF.PI;
                    timer.Restart();
                }
            }
        }

        private void AddBullet(Vector2 direction, Vector2 offset)
        {
            var bullet = Bullet.Clone() as Projectile;
            bullet.Position = Position + offset;
            bullet.Parent = this;
            bullet.Direction = direction;
            ProjectileManager.Add(bullet);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
