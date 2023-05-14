using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace MyGame
{
    public class Bullet
    {
        public static Texture2D texture;
        Vector2 Position { get; set; }
        Vector2 Speed { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }
        public bool IsHidden
        {
            get
            {
                return Position.X > Width || Position.X < 0 || Position.Y > Height || Position.Y < 0;
            }
        }

        public Bullet(Vector2 position, Vector2 speed)
        {
            Position = position;
            Speed = speed;
        }

        public void Update()
        {
            Position += Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
