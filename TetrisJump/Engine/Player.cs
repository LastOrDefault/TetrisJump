using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TetrisJump.Engine.Textures;

namespace TetrisJump.Engine
{
    public class Player
    {
        public Point Position { get; set; }
        public Point Size { get; set; }
        public string Texture { get; set; }
        public List<Vector2> Movement { get; set; }

        public Player(Point position, Point size)
        {
            Position = position;
            Size = size;
            Texture = "Player";
            Movement = new List<Vector2>();
        }

        public void AddForce(Vector2 vector)
        {
            Movement.Add(vector);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TextureManager.Draw(spriteBatch, Texture, Position, Size);
        }
    }
}
