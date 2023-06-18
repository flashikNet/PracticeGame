using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Other;
using MyGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Managers
{
    public class EndManager : Sprites.Model
    {
        public int CountryNumber { get; private set; }
        public Texture2D Texture { get; set; }
        public EndManager(int countryNumber)
        {
            CountryNumber = countryNumber;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            spriteBatch.DrawString(MainGameManager.Font, $"You saved {Countries.CountriesList[CountryNumber]}!",
                new Vector2(350, 540), Color.White);
            spriteBatch.DrawString(MainGameManager.Font, $"The world can sleep in peace",
                new Vector2(750, 600), Color.White);
            spriteBatch.DrawString(MainGameManager.Font, $"Press ESC to exit",
                new Vector2(1400, 1030), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            return;
        }
    }
}
