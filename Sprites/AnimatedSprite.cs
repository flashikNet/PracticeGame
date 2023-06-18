using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Sprites
{
    public class AnimatedSprite : Sprite
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        protected float rotateAngle { get; set; } = 0;
        public float Speed { get; set; } = 200f;
        protected Color color = Color.White;
        int currentFrame;
        int totalFrames;
        int counter = 1;
        int period;

        public new Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }
        public AnimatedSprite(Texture2D texture, int rows, int columns, int period = 5) : base(texture)
        {
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = rows * columns;
            Width = Texture.Width / Columns;
            Height = Texture.Height / Rows;
            this.period = period;
            Origin = new Vector2(Width / 2, Height / 2);
        }

        public AnimatedSprite(Texture2D texture, Vector2 position, int rows, int columns, int period = 5) : base(texture, position)
        {
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = rows * columns;
            Width = Texture.Width / Columns;
            Height = Texture.Height / Rows;
            this.period = period;
            Origin = new Vector2(Width / 2, Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (counter % period == 0)
            {
                currentFrame++;
                counter++;
            }
            counter++;
            if (currentFrame == totalFrames) currentFrame = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int row = currentFrame / Columns;
            int col = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(Width * col, Height * row, Width, Height);
            Rectangle destinationRectanfle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            spriteBatch.Draw(Texture, destinationRectanfle, sourceRectangle, color, rotateAngle,
                Origin, SpriteEffects.None, 0f);
        }
    }
}
