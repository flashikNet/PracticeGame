using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Other;
using MyGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Managers
{
    public class StartManager : Sprites.Model
    {
        public int CountryNumber { get; private set; }
        public InputManager InputManager { get; set; }
        public Texture2D Texture { get; set; }

        public StartManager()
        {
            CountryNumber = MainGameManager.Random.Next(Countries.CountriesList.Length);
        }

        public StartManager(int countryNumber)
        {
            CountryNumber = countryNumber;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            spriteBatch.DrawString(MainGameManager.Font, $"{Countries.CountriesList[CountryNumber]} is attacked by zombies!",
                new Vector2(250, 440), Color.White);
            spriteBatch.DrawString(MainGameManager.Font, "Press space button and save the world!",
                new Vector2(650, 950), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyDown(Keys.Space))
            {
                MainGameManager.State = State.Game;
            }
        }
    }
}
