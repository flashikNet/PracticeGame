using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player : AnimatedSprite
    {
        public int HealthPoint { get; set; } = 300;
        public Player(Texture2D texture, int rows, int columns, int period) :base(texture, rows, columns, period)
        {

        }

        public override void Move(GameTime gameTime)
        {
            base.Move(gameTime);
            Position = Vector2.Clamp(Position, Vector2.Zero, new Vector2(MainGame.Width, MainGame.Height));
        }
    }
}
