using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TetrisJump.Engine.Tiles;

namespace TetrisJump.Engine
{
    public static class Screen
    {
        public static Point Size { get; set; }
        public static GameTick Tick { get; set; }
        public static GameTick TetrisTick { get; set; }
        public static Level Level { get; set; }
        public static TetrisTile Tile { get; set; }
        public static GameState State { get; set; }

        static Screen()
        {
            Tick = new GameTick(50);
            Tick.tick += Tick_tick;
            TetrisTick = new GameTick(2);
            TetrisTick.tick += TetrisTick_tick;
            TetrisTick.IsFreezed = true;
        }

        public static void Initialize()
        {
            Level = new Level("test.level");
        }

        private static void TetrisTick_tick(GameTime gameTime)
        {
            
        }

        private static void Tick_tick(GameTime gameTime)
        {
            switch (State)
            {
                case GameState.Ready:
                    break;
                case GameState.Tetris:
                    break;
                case GameState.Jump:
                    break;
                case GameState.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void Update(GameTime gameTime)
        {
            Tick.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            Level.Map.Draw(spriteBatch);
        }

        public static bool MoveTileDown()
        {
            throw new NotImplementedException();
        }

        public enum GameState
        {
            Ready,
            Tetris,
            Jump,
            End
        }
    }
}
