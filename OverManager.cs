using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class OverManager : Model
    {
        public int CountryNumber { get;private set; }
        public Texture2D Texture { get; set; }
        public OverManager(int countryNumber) { 
            CountryNumber = countryNumber;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,Vector2.Zero, Color.White);
            spriteBatch.DrawString(MainGame.Font, $"YOU'RE DEAD!",
                new Vector2(800, 240), Color.Red);
            spriteBatch.DrawString(MainGame.Font, $"{Countries.CountriesList[CountryNumber]} was destroyed!",
                new Vector2(300,350), Color.White);
            spriteBatch.DrawString(MainGame.Font, $"Other countries are next!",
                new Vector2(800, 500), Color.White);
            spriteBatch.DrawString(MainGame.Font, $"Press ESC to exit",
                new Vector2(1400, 1030), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            return;
        }
    }
}
