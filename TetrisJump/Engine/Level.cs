using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using TetrisJump.Engine.Tiles;

namespace TetrisJump.Engine
{
    public class Level
    {
        public Point Size { get; set; }
        public Point Start { get; set; }
        public Point Goal { get; set; }
        public List<Tile> StartTiles { get; set; }
        public List<Tile> DroppingTiles { get; set; }
        public Map Map { get; set; }

        public Level(string path)
        {
            var xml = new XmlSerializer(typeof(LevelFile));
            LevelFile lf;
            using (var reader = File.OpenRead(path))
            {
                lf = (LevelFile) xml.Deserialize(reader);
            }

            Size = lf.Size;
            Map = new Map();
            Map.Tiles = new Tile[Size.X, Size.Y];
            var rnd = new Random();
            foreach (var t in lf.Tiles)
            {
                Map.Tiles[t.X, t.Y] = new Tile((((Tile.Textures)rnd.Next(0,4)).ToString()));
            }
        }
    }
}
