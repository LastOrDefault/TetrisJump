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
        const int TILESIZE = 64;


        public void Draw(SpriteBatch spriteBatch)
        {
            var offset = new Point((Screen.Size.X - Tiles.GetLength(0) * TILESIZE) / 2, (Screen.Size.Y - Tiles.GetLength(1) * TILESIZE) / 2);
            TextureManager.Draw(spriteBatch, "Background", offset,
                new Point(Tiles.GetLength(0) * TILESIZE, Tiles.GetLength(1) * TILESIZE));
            for (var x = 0; x < Tiles.GetLength(0); x++)
            {
                for (var y = 0; y < Tiles.GetLength(1); y++)
                {
                    if(Tiles[x,y] != null)
                        TextureManager.Draw(spriteBatch, Tiles[x, y].Texture,
                         new Point(offset.X + x * TILESIZE, offset.Y + y * TILESIZE),
                         new Point(TILESIZE));
                }
            }
        }
    }
}
