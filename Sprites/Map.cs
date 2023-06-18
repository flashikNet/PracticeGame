using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Sprites
{
    public class Map
    {
        public readonly Point mapTileSize;
        private readonly Sprite[,] tiles;
        public Vector2 TileSize { get; private set; }
        public Vector2 MapSize { get; private set; }
        public float Scale { get; init; } = 4f;

        public Map(List<Texture2D> textures, int xSize, int ySize)
        {
            mapTileSize = new Point(xSize, ySize);
            tiles = new Sprite[mapTileSize.X, mapTileSize.Y];
            TileSize = new(textures[0].Width * Scale, textures[0].Height * Scale);
            MapSize = new(TileSize.X * mapTileSize.X, TileSize.Y * mapTileSize.Y);

            Random random = new();

            for (int y = 0; y < mapTileSize.Y; y++)
            {
                for (int x = 0; x < mapTileSize.X; x++)
                {
                    int r = random.Next(0, textures.Count);
                    tiles[x, y] = new(textures[r], new(x * TileSize.X, y * TileSize.Y)) { Scale = Scale };
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < mapTileSize.Y; y++)
            {
                for (int x = 0; x < mapTileSize.X; x++) tiles[x, y].Draw(spriteBatch);
            }
        }
    }
}
