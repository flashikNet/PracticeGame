using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MyGame
{
    public class MainGame : Game
    {
        SpriteFont scoreFont;
        int score;
        Player player;
        List<Bullet> bullets;
        Stopwatch timer = new Stopwatch();
        int rateOfFire = 1000/10;
        int bulletSpeed = 10;
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Height = graphics.PreferredBackBufferHeight;
            Width = graphics.PreferredBackBufferWidth;
        }

        protected override void Initialize()
        {
            bullets = new List<Bullet>();
            Bullet.Width = Width;
            Bullet.Height = Height;
            timer.Start();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            scoreFont = Content.Load<SpriteFont>("score");
            Bullet.texture = Content.Load<Texture2D>("bots_bullet");
            this.player = new Player(Content.Load<Texture2D>("moveWithGun"), 4, 5, 5)
            {
                Input = new Controller
                {
                    Down = Keys.S,
                    Up = Keys.W,
                    Left = Keys.A,
                    Right = Keys.D,
                    Speedup = Keys.LeftShift,
                },
                Position = new Vector2(Width / 2, Height / 2),
                Speed = 300f
            };

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Up))
            {
                player.RotateAngle = -1.57f;
                if (timer.ElapsedMilliseconds > rateOfFire)
                {
                    bullets.Add(new Bullet(player.Position + new Vector2(50,-110), new Vector2(0, -bulletSpeed)));
                    timer.Restart();
                }
            }
            else if (kState.IsKeyDown(Keys.Down))
            {
                player.RotateAngle = 1.57f;
                if (timer.ElapsedMilliseconds > rateOfFire)
                {
                    bullets.Add(new Bullet(player.Position + new Vector2(-58,110), new Vector2(0, bulletSpeed)));
                    timer.Restart();
                }
            }
            else if (kState.IsKeyDown(Keys.Left))
            {
                player.RotateAngle = 3.14f;
                if (timer.ElapsedMilliseconds > rateOfFire)
                {
                    bullets.Add(new Bullet(player.Position + new Vector2(-115, -58), new Vector2(-bulletSpeed, 0)));
                    timer.Restart();
                }
            }
            else if (kState.IsKeyDown(Keys.Right))
            {
                player.RotateAngle = 0f;
                if (timer.ElapsedMilliseconds > rateOfFire)
                {
                    bullets.Add(new Bullet(player.Position + new Vector2(110, 50), new Vector2(bulletSpeed, 0)));
                    timer.Restart();
                }
            }
            //if (kState.IsKeyDown(Keys.Up) && kState.IsKeyDown(Keys.Right))//вверх-вправо
            //{
            //    texture.RotateAngle = -0.79f;
            //    bullets.Add(new Bullet(Position, new Vector2(1, -1)));
            //}
            //if (kState.IsKeyDown(Keys.Down) && kState.IsKeyDown(Keys.Right))//вниз-вправо
            //{
            //    texture.RotateAngle = 0.79f;
            //    bullets.Add(new Bullet(Position, new Vector2(1, 1)));
            //}
            //if (kState.IsKeyDown(Keys.Up) && kState.IsKeyDown(Keys.Left))//вверх-влево
            //{
            //    texture.RotateAngle = -2.36f;
            //    bullets.Add(new Bullet(Position, new Vector2(-1, -1)));
            //}
            //if (kState.IsKeyDown(Keys.Down) && kState.IsKeyDown(Keys.Left))//вниз-влево
            //{
            //    texture.RotateAngle = 2.36f;
            //    bullets.Add(new Bullet(Position, new Vector2(-1, 1)));
            //}

            //if (Position.X < player.Width / 2) Position.X = player.Width / 2;
            //else if(Position.X > graphics.PreferredBackBufferWidth - player.Width /2)
            //    Position.X = graphics.PreferredBackBufferWidth - player.Width/2;
            //if (Position.Y < player.Height / 2) Position.Y = player.Height / 2;
            //else if (Position.Y > graphics.PreferredBackBufferHeight - player.Height / 2)
            //    Position.Y = graphics.PreferredBackBufferHeight - player.Height / 2;
            if ((Math.Abs(player.Position.X - 0) < 200 || Math.Abs(player.Position.X - 800) < 200)
                && (Math.Abs(player.Position.Y - 240) < 200)) score++;
            //if (flag)
            //{
            //    Speed /= 3;
            //    flag = false;
            //}
            player.Update(gameTime);
            for (var i = 0; i < bullets.Count;i++)
            {
                bullets[i].Update();
                if (bullets[i].IsHidden)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.YellowGreen);

            spriteBatch.Begin();

            foreach(var bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            spriteBatch.DrawString(scoreFont, "Score: " + score, new Vector2(50, 50), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}