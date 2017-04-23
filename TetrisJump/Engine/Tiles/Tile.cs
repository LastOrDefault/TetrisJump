using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisJump.Engine.Tiles
{
    public class Tile
    {
        public Tile(string texture)
        {
            Texture = texture;
        }

        public string Texture { get; set; }

        public enum Textures
        {
            Red,
            Blue,
            Green,
            Yellow,
            Orange
        }
    }
}
