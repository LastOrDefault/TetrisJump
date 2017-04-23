using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisJump.Engine.Tiles
{
    public static class TetrisTile
    {
        public static Point[] GetTetrisTiles(Point start, byte id)
        {
            switch (id)
            {
                case 0:
                    return new []
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X + 1, start.Y + 0),
                        new Point(start.X + 0, start.Y + 1),
                        new Point(start.X + 1, start.Y + 1)
                    };
                case 1:
                    return new[]
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X - 1, start.Y + 0),
                        new Point(start.X + 1, start.Y + 0),
                        new Point(start.X + 2, start.Y + 0)
                    };
                case 2:
                    return new[]
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X + 0, start.Y + 1),
                        new Point(start.X + 0, start.Y + 2),
                        new Point(start.X + 0, start.Y + 3)
                    };
                case 3:
                    return new[]
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X + 0, start.Y + 1),
                        new Point(start.X + 0, start.Y + 2),
                        new Point(start.X + 1, start.Y + 2)
                    };
                case 4:
                    return new[]
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X + 0, start.Y + 1),
                        new Point(start.X - 1, start.Y + 1),
                        new Point(start.X - 2, start.Y + 1)
                    };
                case 5:
                    return new[]
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X - 1, start.Y + 0),
                        new Point(start.X + 0, start.Y + 1),
                        new Point(start.X + 0, start.Y + 2)
                    };
                case 6:
                    return new[]
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X - 1, start.Y + 0),
                        new Point(start.X + 1, start.Y + 0),
                        new Point(start.X - 1, start.Y + 1)
                    };
                case 7:
                    return new[]
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X + 0, start.Y + 1),
                        new Point(start.X + 1, start.Y + 1),
                        new Point(start.X + 2, start.Y + 1)
                    };
                case 8:
                    return new[]
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X + 1, start.Y + 0),
                        new Point(start.X + 0, start.Y + 1),
                        new Point(start.X + 0, start.Y + 2)
                    };
                case 9:
                    return new[]
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X - 1, start.Y + 0),
                        new Point(start.X + 1, start.Y + 0),
                        new Point(start.X + 1, start.Y + 1)
                    };
                case 10:
                    return new[]
                    {
                        new Point(start.X + 0, start.Y + 0),
                        new Point(start.X + 0, start.Y + 1),
                        new Point(start.X + 0, start.Y + 2),
                        new Point(start.X - 1, start.Y + 2)
                    };   
                default:
                    throw new Exception(":(");

            }
        }
    }
}
