using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TetrisJump.Engine.Tiles;

namespace TetrisJump.Engine
{
    public class LevelFile
    {
        public LevelFile()
        {
            Tiles = new List<Point>();
        }

        public Point Size { get; set; }
        public List<Point> Tiles { get; set; }
    }
}
