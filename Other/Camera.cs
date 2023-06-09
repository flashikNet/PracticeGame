﻿using Microsoft.Xna.Framework;
using MyGame.Managers;
using MyGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Other
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public Map Map { get; init; }
        public void Follow(Sprite target)
        {
            var dx = MainGameManager.Width / 2 - target.Position.X;
            var dy = MainGameManager.Height / 2 - target.Position.Y;
            if (Map is not null) CalculateBounds(ref dx, ref dy);
            Transform = Matrix.CreateTranslation(dx, dy, 0f);
        }

        private void CalculateBounds(ref float dx, ref float dy)
        {
            dx = MathHelper.Clamp(dx, -Map.MapSize.X + MainGameManager.Width + Map.TileSize.X / 2, Map.TileSize.X / 2);
            dy = MathHelper.Clamp(dy, -Map.MapSize.Y + MainGameManager.Height + Map.TileSize.Y / 2, Map.TileSize.Y / 2);
        }
    }
}
