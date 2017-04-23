using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TetrisJump.Engine.Textures;

namespace TetrisJump.Engine.Tiles
{
    public class Map
    {
        public Tile[,] Tiles { get; set; }
        public Point Offset { get; set; }
        public int Tilesize = 40;


        public void Draw(SpriteBatch spriteBatch)
        {
            TextureManager.Draw(spriteBatch, "Background", Offset,
                new Point(Tiles.GetLength(0) * Tilesize, Tiles.GetLength(1) * Tilesize));
            for (var x = 0; x < Tiles.GetLength(0); x++)
            {
                for (var y = 0; y < Tiles.GetLength(1); y++)
                {
                    if(Tiles[x,y] != null)
                        TextureManager.Draw(spriteBatch, Tiles[x, y].Texture,
                         new Point(Offset.X + x * Tilesize, Offset.Y + y * Tilesize),
                         new Point(Tilesize));
                }
            }
        }
    }
}
