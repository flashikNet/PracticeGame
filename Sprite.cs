using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Sprite : Models
    {
        public Texture2D Texture { get; protected set; }
        public Vector2 Position;
        public float Speed { get; set; } = 2f;
        int boost = 3;
        bool IsSpeedUp;
        public Vector2 Origin;
        public float Scale { get; set; } = 1f;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
        public Controller Input { get; set; }
        public Sprite(Texture2D texture)
        {
            this.Texture = texture;
            Origin = new Vector2(texture.Width/2, texture.Height/2);
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            Position = position;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        public virtual void Move(GameTime gameTime)
        { 
            if (Input == null) return;

            var kstate = Keyboard.GetState();
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (kstate.IsKeyDown(Input.Speedup))
            {
                Speed *= boost;
                IsSpeedUp = true;
            }
            if (kstate.IsKeyDown(Input.Left))
                Position.X -= Speed * time;
            if (kstate.IsKeyDown(Input.Right))
                Position.X += Speed * time;
            if (kstate.IsKeyDown(Input.Up))
                Position.Y -= Speed * time;
            if (kstate.IsKeyDown(Input.Down))
                Position.Y += Speed * time;
            if(IsSpeedUp)
            {
                Speed /= boost;
                IsSpeedUp = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
