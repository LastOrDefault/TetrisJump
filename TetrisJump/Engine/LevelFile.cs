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
            TetrisTiles = new List<byte>();
        }

        public Point Size { get; set; }
        public List<Point> Tiles { get; set; }
        public List<byte> TetrisTiles { get; set; }
        public Point Start { get; set; }
        public Point Goal { get; set; }
    }
}
