using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class AnimatedSprite : Sprite
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public float RotateAngle { get; set; } = 0;
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
        public AnimatedSprite(Texture2D texture, int rows, int columns, int period) : base(texture)
        {
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = rows * columns;
            Width = Texture.Width / Columns;
            Height = Texture.Height / Rows;
            this.period = period;
            Origin = new Vector2 (Width/2, Height/2);
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
            Rectangle sourceRectangle = new Rectangle(Width*col, Height*row, Width, Height);
            Rectangle destinationRectanfle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            spriteBatch.Draw(Texture, destinationRectanfle, sourceRectangle, Color.White, RotateAngle,
                Origin, SpriteEffects.None, 0f);
        }
    }
}
