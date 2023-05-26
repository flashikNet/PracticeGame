using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public static List<Models> Sprites = new List<Models>();
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        List<Texture2D> backTextures = new List<Texture2D>();
        Stopwatch timer = new Stopwatch();
        SpriteFont font;
        Camera camera;
        Map map;
        Player player;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            timer.Start();
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.ApplyChanges();
            Height = graphics.PreferredBackBufferHeight;
            Width = graphics.PreferredBackBufferWidth;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("score");
            for (int i = 1; i < 62; i++)
                backTextures.Add(Content.Load<Texture2D>($"image_part_{i.ToString().PadLeft(3,'0')}"));
            var bulletTexture = Content.Load<Texture2D>("bullet");
            map = new Map(backTextures, 100, 100);
            player = new Player(Content.Load<Texture2D>("moveWithGun"), 4, 5, 5, new Bullet(bulletTexture) { Speed = 20 })
            {
                Input = new Controller
                {
                    Down = Keys.S,
                    Up = Keys.W,
                    Left = Keys.A,
                    Right = Keys.D,
                    Speedup = Keys.LeftShift,
                    ShootUp = Keys.Up,
                    ShootDown = Keys.Down,
                    ShootLeft = Keys.Left,
                    ShootRight = Keys.Right,
                },
                Position = new Vector2(Width / 2, Height / 2),
                Speed = 300f,
                Origin = new Vector2(104, 121),
                Offset = new Vector2(127, 42),
                UseRate = 100,
            };
            player.SetBounds(map);
            Sprites.Add(player);
            camera = new Camera() { Map = map};

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            for (var i = 0; i < Sprites.Count; i++)
            {
                Sprites[i].Update(gameTime);
                if (Sprites[i] is Bullet b && b.IsDone)
                {
                    Sprites.RemoveAt(i);
                    i--;
                }
            }
            camera.Follow(player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.YellowGreen);
            spriteBatch.Begin(transformMatrix: camera.Transform);
            map.Draw(spriteBatch);
            foreach (var sprite in Sprites)
                sprite.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "Score: " + 100, new Vector2(50, 50), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}