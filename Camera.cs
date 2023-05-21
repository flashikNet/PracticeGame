using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public void Follow(Sprite target)
        {
            var offset = Matrix.CreateTranslation(
                                MainGame.Width / 2,
                                MainGame.Height / 2,
                                0);
            var position = Matrix.CreateTranslation(
                            -target.Position.X,
                            -target.Position.Y,
                            0);
            Transform = position * offset;
        }
    }
}
