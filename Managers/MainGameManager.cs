using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MyGame.Other;
using MyGame.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MyGame.Managers
{

    public enum State
    {
        Start,
        Game,
        End,
        Over,
    }
    public class MainGameManager : Game
    {
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public static Random Random { get; private set; }
        public static SpriteFont Font { get; private set; }
        public static Player Player { get; private set; }
        public static State State { get; set; } = State.Start;
        List<Texture2D> backTextures = new List<Texture2D>();
        Camera camera;
        Map map;
        InputManager inputManager;
        ProjectileManager projectileManager;
        EnemyManager enemyManager;
        StartManager startManager;
        EndManager endManager;
        OverManager overManager;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public MainGameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.ApplyChanges();
            Height = graphics.PreferredBackBufferHeight;
            Width = graphics.PreferredBackBufferWidth;
            Random = new Random();
            inputManager = new InputManager();
            startManager = new StartManager() { InputManager = inputManager };
            var countryNumber = startManager.CountryNumber;
            endManager = new EndManager(countryNumber);
            overManager = new OverManager(countryNumber);
            MediaPlayer.Play(Content.Load<Song>("ambient"));
            MediaPlayer.IsRepeating = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("font");
            for (int i = 1; i < 62; i++)
                backTextures.Add(Content.Load<Texture2D>($"image_part_{i.ToString().PadLeft(3, '0')}"));
            var bulletTexture = Content.Load<Texture2D>("bullet");
            map = new Map(backTextures, 100, 100);
            projectileManager = new ProjectileManager();
            Player = new Player(Content.Load<Texture2D>("player"), 4, 5, 5,
                new Projectile(bulletTexture) { Speed = 20 })
            {
                ProjectileManager = projectileManager,
                Input = inputManager,
                Position = map.MapSize / 2,
                Speed = 300f,
                Origin = new Vector2(104, 121),
                Offset = new Vector2(127, 42),
                UseRate = 200,
            };
            Player.SetBounds(map);
            enemyManager = new EnemyManager(Content.Load<Texture2D>("enemy"), 100, 500);
            camera = new Camera() { Map = map };
            startManager.Texture = Content.Load<Texture2D>("start");
            overManager.Texture = Content.Load<Texture2D>("gameOver");
            endManager.Texture = Content.Load<Texture2D>("good");

        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.Update();
            if (inputManager.IsKeyDown(Keys.Escape))
                Exit();
            if (State == State.Game)
            {
                enemyManager.Update(gameTime, Player);
                projectileManager.Update(gameTime, enemyManager.enemyList);
                Player.Update(gameTime);
                camera.Follow(Player);
            }
            else if (State == State.Start)
            {
                startManager.Update(gameTime);
            }
            else if (State == State.End)
            {
                endManager.Update(gameTime);
            }
            else if (State == State.Over)
            {
                overManager.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);
            if (State == State.Game)
            {
                spriteBatch.Begin(transformMatrix: camera.Transform);
                map.Draw(spriteBatch);
                spriteBatch.DrawString(Font, "Use WASD, left shift and arrows", map.MapSize / 2 - new Vector2(400, 300), Color.White);
                enemyManager.Draw(spriteBatch);
                projectileManager.Draw(spriteBatch);
                Player.Draw(spriteBatch);
                spriteBatch.DrawString(Font, $"Boost: {Player.ShiftTime}ms", new Vector2(400, 350) + Player.Position, Color.White);
                spriteBatch.DrawString(Font, $"Enemies: {enemyManager.LeftCount}", new Vector2(400, 400) + Player.Position, Color.White);
                spriteBatch.DrawString(Font, $"HP: {Player.HealthPoint}", new Vector2(400, 450) + Player.Position,
                    Player.HealthPoint > 20 ? Color.White : Color.Red);
            }
            else if (State == State.Start)
            {
                spriteBatch.Begin();
                startManager.Draw(spriteBatch);
            }
            else if (State == State.End)
            {
                spriteBatch.Begin();
                endManager.Draw(spriteBatch);
            }
            else if (State == State.Over)
            {
                spriteBatch.Begin();
                overManager.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}