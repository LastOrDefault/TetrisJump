using Microsoft.Xna.Framework;

namespace TetrisJump
{
    public class GameTick
    {
        int elapsedTime { get; set; }

        /// <summary>
        /// Spielstop
        /// </summary>
        public bool IsFreezed { get; set; } = false;

        readonly int TicksPerSecond;

        /// <summary>
        /// 
        /// </summary>
        public event TickEventHandler tick;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public delegate void TickEventHandler(GameTime gameTime);

        /// <summary>
        /// Neuer GameTick
        /// </summary>
        public GameTick(int ticksPerSecond)
        {
            elapsedTime = 0;
            TicksPerSecond = ticksPerSecond;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (IsFreezed) return;
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime <= 1000 / TicksPerSecond) return;
            elapsedTime = 0;
            tick?.Invoke(gameTime);
        }
    }
}
