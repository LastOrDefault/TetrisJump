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
        public List<byte> DroppingTiles { get; set; }
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
            Start = lf.Start;
            Goal = lf.Goal;
            DroppingTiles = lf.TetrisTiles;
            Map = new Map();
            Map.Tiles = new Tile[Size.X, Size.Y];
            Map.Tiles[Start.X, Start.Y] = new Tile("Start");
            Map.Tiles[Goal.X, Goal.Y] = new Tile("Goal");
            var rnd = new Random();
            foreach (var t in lf.Tiles)
            {
                Map.Tiles[t.X, t.Y] = new Tile((((Tile.Textures)rnd.Next(0,6)).ToString()));
            }
            if (Map.Tiles.GetLength(0) > Map.Tiles.GetLength(1))
                Map.Tilesize = Screen.Size.X / Map.Tiles.GetLength(0) - 5;
            else
                Map.Tilesize = Screen.Size.Y / Map.Tiles.GetLength(1) - 5;
            if (Map.Tilesize > 64)
                Map.Tilesize = 64;

            Map.Offset = new Point((Screen.Size.X - Map.Tiles.GetLength(0) * Map.Tilesize) / 2, (Screen.Size.Y - Map.Tiles.GetLength(1) * Map.Tilesize) / 2);
        }
    }
}
