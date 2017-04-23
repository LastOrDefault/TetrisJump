using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TetrisJump.Engine.Textures;
using TetrisJump.Engine.Tiles;

namespace TetrisJump.Engine
{
    public static class Screen
    {
        public static Point Size { get; set; }
        public static GameTick Tick { get; set; }

        public static GameTick TetrisTick { get; set; }
        public static Level Level { get; set; }
        public static byte LevelId { get; set; }
        public static Point[] TetrisTiles;
        public static GameState State { get; set; }
        public static string BufferInput { get; set; }

        public static Player Player { get; set; }
        public static int Jump { get; set; }
        public static bool OnGround { get; set; }

        static Screen()
        {
            Tick = new GameTick(50);
            Tick.tick += Tick_tick;
            TetrisTick = new GameTick(1.5);
            TetrisTick.tick += TetrisTick_tick;

            TetrisTick.IsFreezed = true;
            State = GameState.Ready;
            BufferInput = "";
            LevelId = 1;
        }

        public static void Initialize()
        {
            Level = new Level($"Levels/level{LevelId}.level");
            State = GameState.Ready;
            TetrisTiles = null;
            TetrisTick.IsFreezed = true;
        }

        private static void TetrisTick_tick(GameTime gameTime)
        {
            if (TetrisTiles == null)
            {
                if (Level.DroppingTiles.Count == 0)
                {
                    StartJumpState();
                    return;
                }
                //Spawn new Tiles
                TetrisTiles = TetrisTile.GetTetrisTiles(
                    new Point(Level.Size.X / 2, 0),
                    Level.DroppingTiles.First());
                Level.DroppingTiles.RemoveAt(0);

                //If place is blocked, start jumpsection
                if(TetrisTiles.Any(x => Level.Map.Tiles[x.X, x.Y] != null))
                    StartJumpState();

                //Insert tiles
                var rnd = new Random().Next(0, 6);
                foreach (var t in TetrisTiles)
                {
                    Level.Map.Tiles[t.X, t.Y] = new Tile(((Tile.Textures) rnd).ToString());
                }
            }
            else
            {
                if (BufferInput == "Left")
                    MoveTileLeft();
                if (BufferInput == "Right")
                    MoveTileRight();
                BufferInput = "";
                if (!MoveTileDown())
                {
                    TetrisTiles = null;
                }
            }
        }

        private static void Tick_tick(GameTime gameTime)
        {
            switch (State)
            {
                case GameState.Ready:
                    if (KeyboardManager.IsKeyPressed("Enter", true))
                    {
                        TetrisTick.IsFreezed = false;
                        State = GameState.Tetris;
                    }
                    break;
                case GameState.Tetris:
                    if (KeyboardManager.IsKeyPressed("A", true))
                        BufferInput = "Left";
                    if (KeyboardManager.IsKeyPressed("D", true))
                        BufferInput = "Right";
                    if (KeyboardManager.IsKeyPressed("R", true))
                    {
                        TextureManager.PlaySound("Restart");
                        Initialize();
                    }
                    //if (KeyboardManager.IsKeyPressed("Enter"))
                    //    StartJumpState();
                    break;
                case GameState.Jump:
                    Player.AddForce(new Vector2(0, Level.Map.Tilesize / 6));//Gravity
                    if (KeyboardManager.IsKeyPressed("A", true))
                    {
                        //Player.Texture = "PlayerLeft";
                        Player.AddForce(new Vector2(-(Level.Map.Tilesize / 4), 0));
                    }
                    if (KeyboardManager.IsKeyPressed("D", true))
                    {
                        //Player.Texture = "PlayerRight";
                        Player.AddForce(new Vector2(Level.Map.Tilesize / 4, 0));
                    }
                    if (KeyboardManager.IsKeyPressed("Space", true))
                    {
                        if (OnGround)
                        {
                            Jump = 14;
                            OnGround = false;
                            TextureManager.PlaySound("Jump");
                        }
                    }
                    if (KeyboardManager.IsKeyPressed("R", true))
                    {
                        TextureManager.PlaySound("Restart");
                        Initialize();
                    }

                    if (Jump > 0)
                    {
                        Player.AddForce(new Vector2(0, - Convert.ToSingle(Level.Map.Tilesize / 3)));
                        Jump--;
                    }

                    MovePlayer();
                    break;
                case GameState.End:
                    if (KeyboardManager.IsKeyPressed("Enter", true) && File.Exists($"Levels/level{LevelId + 1}.level"))
                    {
                        LevelId++;
                        Initialize();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void Update(GameTime gameTime)
        {
            Tick.Update(gameTime);
            TetrisTick.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            Level.Map.Draw(spriteBatch);
            spriteBatch.DrawString(TextureManager.Font, $"Level: {LevelId}", Vector2.Zero, Color.Black);

            if (State == GameState.Jump || State == GameState.End)
            {
                Player.Draw(spriteBatch);
                spriteBatch.DrawString(TextureManager.Font, $"Press R to restart", new Vector2(Size.X / 2 - 30, 0), Color.Black);
            }

            if (State == GameState.End)
            {
                if (File.Exists($"Levels/level{LevelId + 1}.level"))
                    spriteBatch.DrawString(TextureManager.BigFont, "Press Enter...", new Vector2(Size.X / 2 - 200, Size.Y / 2), Color.Black);
                else
                    spriteBatch.DrawString(TextureManager.BigFont, "Thank you for playing!", new Vector2(Size.X / 2 - 200, Size.Y / 2), Color.Black);
            }

            if (State == GameState.Ready)
            {
               spriteBatch.DrawString(TextureManager.BigFont, "Press Enter to Start", new Vector2(Size.X / 2 - 200, Size.Y / 2), Color.Black);
                spriteBatch.DrawString(TextureManager.BigFont, "Read readme.txt for controls!", new Vector2(Size.X / 2 - 200, Size.Y / 2 + 50), Color.Black);
            }
        }

        public static bool MoveTileDown()
        {
            foreach (var t in TetrisTiles)
            {
                if (t.Y + 1 >= Level.Size.Y || Level.Map.Tiles[t.X, t.Y + 1] != null && !TetrisTiles.Any(x => x.Y == t.Y + 1 && x.X == t.X))
                {
                    TextureManager.PlaySound("Drop");
                    return false;
                }
            }
            foreach (var t in TetrisTiles)
            {
                Level.Map.Tiles[t.X, t.Y + 1] = new Tile(Level.Map.Tiles[t.X, t.Y].Texture);
                if(!TetrisTiles.Any(x => x.Y == t.Y -1 && t.X == x.X))
                    Level.Map.Tiles[t.X, t.Y] = null;
            }
            var i = 0;
            foreach (var t in TetrisTiles)
            {
                TetrisTiles[i] = new Point(t.X, t.Y + 1);
                i++;
            }
            return true;
        }

        public static bool MoveTileLeft()
        {
            foreach (var t in TetrisTiles)
            {
                if (t.X - 1 < 0 || Level.Map.Tiles[t.X - 1, t.Y] != null && !TetrisTiles.Any(x => x.Y == t.Y && x.X == t.X - 1))
                {
                    return false;
                }
            }
            foreach (var t in TetrisTiles)
            {
                Level.Map.Tiles[t.X - 1, t.Y] = new Tile(Level.Map.Tiles[t.X, t.Y].Texture);
                if (!TetrisTiles.Any(x => x.Y == t.Y && t.X + 1 == x.X))
                    Level.Map.Tiles[t.X, t.Y] = null;
            }
            var i = 0;
            foreach (var t in TetrisTiles)
            {
                TetrisTiles[i] = new Point(t.X - 1, t.Y);
                i++;
            }
            return true;
        }

        public static bool MoveTileRight()
        {
            foreach (var t in TetrisTiles)
            {
                if (t.X + 1 >= Level.Size.X 
                    || Level.Map.Tiles[t.X + 1, t.Y] != null 
                        && !TetrisTiles.Any(x => x.Y == t.Y && x.X == t.X + 1))
                {
                    return false;
                }
            }
            foreach (var t in TetrisTiles)
            {
                Level.Map.Tiles[t.X + 1, t.Y] = new Tile(Level.Map.Tiles[t.X, t.Y].Texture);
                if (!TetrisTiles.Any(x => x.Y == t.Y && t.X - 1 == x.X))
                    Level.Map.Tiles[t.X, t.Y] = null;
            }
            var i = 0;
            foreach (var t in TetrisTiles)
            {
                TetrisTiles[i] = new Point(t.X + 1, t.Y);
                i++;
            }
            return true;
        }

        public static void MovePlayer()
        {
            foreach (var v in Player.Movement)
            {
                if (new Rectangle(
                        Level.Map.Offset.X + Level.Goal.X * Level.Map.Tilesize,
                        Level.Map.Offset.Y + Level.Goal.Y * Level.Map.Tilesize,
                        Level.Map.Tilesize,
                        Level.Map.Tilesize)
                    .Intersects(new Rectangle((v + Player.Position.ToVector2()).ToPoint(), Player.Size)))
                        EndGame();

                var vector = v;
                if (State != GameState.Jump) continue;
                var res = (vector + Player.Position.ToVector2()).ToPoint();

                if (res.X < Level.Map.Offset.X ||
                    res.Y < Level.Map.Offset.Y ||
                    res.X + Player.Size.X >
                    Level.Map.Offset.X + Level.Map.Tiles.GetLength(0) * Level.Map.Tilesize ||
                    res.Y + Player.Size.Y >
                    Level.Map.Offset.Y + Level.Map.Tiles.GetLength(1) * Level.Map.Tilesize)
                {
                    if (v.Y > 0)
                        OnGround = true;
                    continue;
                }

                for (var x = 0; x < Level.Map.Tiles.GetLength(0); x++)
                {
                    for (var y = 0; y < Level.Map.Tiles.GetLength(1); y++)
                    {
                        if (Level.Map.Tiles[x, y] != null)
                        {
                            if (new Rectangle(
                                    Level.Map.Offset.X + x * Level.Map.Tilesize,
                                    Level.Map.Offset.Y + y * Level.Map.Tilesize,
                                    Level.Map.Tilesize,
                                    Level.Map.Tilesize)
                                .Intersects(new Rectangle(res.X + 4, res.Y + 4, Player.Size.X - 8, Player.Size.Y - 8)))
                            {
                                vector = Vector2.Zero;
                                if (v.Y > 0)
                                    OnGround = true;
                            }
                        }
                    }
                }
                Player.Position += vector.ToPoint();
            }
            Player.Movement.Clear();
        }

        private static void EndGame()
        {
            TextureManager.PlaySound("Level");
            State = GameState.End;

        }

        public static void StartJumpState()
        {
            State = GameState.Jump;
            TetrisTick.IsFreezed = true;
            //Replace Starttile
            Level.Map.Tiles[Level.Start.X, Level.Start.Y] = null;
            //Spawn Player
            Player = new Player(
                new Point(Level.Map.Offset.X + Level.Start.X * Level.Map.Tilesize,
                          Level.Map.Offset.Y + Level.Start.Y * Level.Map.Tilesize),
                new Point(Level.Map.Tilesize));
            OnGround = false;
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
