using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TetrisJump.Engine.Textures
{
    public static class TextureManager
    {
        public static ContentManager Content;
        public static Dictionary<string, Animation> Textures { get; } = new Dictionary<string, Animation>();
        public static Dictionary<string, Song> Sounds { get; } = new Dictionary<string, Song>();
        public static SpriteFont Font { get;  set; }
        public static SpriteFont BigFont { get; set; }

        public static void LoadTexture(string id, string path, int count = 1, int time = 0, bool repeat = true)
        {
            Textures.Add(id, new Animation(Content.Load<Texture2D>(path), count, time, repeat));
        }

        public static void LoadSound(string id, string path)
        {
            Sounds.Add(id, Content.Load<Song>(path));
        }

        public static void PlaySound(string id)
        {
            MediaPlayer.Play(Sounds[id]);
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var a in Textures.Where(x => x.Value.IsRunning))
            {
                a.Value.Update(gameTime);
            }
        }

        public static void StartAnimation(string id)
        {
            Textures[id].IsRunning = true;
        }

        public static void UnloadTexture(string id)
        {
            Textures.Remove(id);
        }

        public static void Draw(SpriteBatch spriteBatch, string id, Point position, Point size)
        {
            Textures[id].Draw(spriteBatch, position, size);
        }
    }
}
