using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisJump.Engine.Textures
{
    public class Animation
    {
        public Texture2D Texture { get; set; }
        private int _count;
        private int _frame = 0;

        public int Time { get; set; }
        private int _timeSinceFrame = 0;
        public bool Repeat { get; set; }
        public bool IsRunning { get; set; }

        public delegate void AnimationFinished(bool repeat);
        public event AnimationFinished AnimationFinishedEvent;

        public Animation(Texture2D texture, int count, int time, bool repeat = true)
        {
            Texture = texture;
            this._count = count;
            Time = time;
            Repeat = repeat;
            IsRunning = repeat;
        }

        public void Update(GameTime gameTime)
        {
            if (Time == 0)
                return;
            _timeSinceFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_timeSinceFrame <= Time) return;
            _timeSinceFrame = 0;
            _frame++;
            if (_frame == _count)
            {
                if (Repeat)
                    _frame = 0;
                else
                    AnimationFinishedEvent?.Invoke(Repeat);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Point position, Point size)
        {
            spriteBatch.Draw(
                Texture,
                new Rectangle(position, size),
                new Rectangle((Texture.Width / _count) * _frame, 0, (Texture.Width / _count), Texture.Height),
                Color.White);
        }
    }
}
