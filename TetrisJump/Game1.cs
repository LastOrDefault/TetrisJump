using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TetrisJump.Engine;
using TetrisJump.Engine.Textures;

namespace TetrisJump
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.ApplyChanges();

            Screen.Size =
                new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            TextureManager.Content = Content;

            Screen.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureManager.LoadTexture("Background", "Background");

            TextureManager.LoadTexture("Red", "Tiles/Red");
            TextureManager.LoadTexture("Blue", "Tiles/Blue");
            TextureManager.LoadTexture("Green", "Tiles/Green");
            TextureManager.LoadTexture("Orange", "Tiles/Orange");
            TextureManager.LoadTexture("Yellow", "Tiles/Yellow");
            TextureManager.LoadTexture("Pink", "Tiles/Pink");
            TextureManager.LoadTexture("Start", "Tiles/Start");
            TextureManager.LoadTexture("Goal", "Tiles/Goal");

            TextureManager.LoadTexture("Player", "Jump/Player", 8, 500);
            TextureManager.LoadTexture("PlayerLeft", "Jump/PlayerLeft", 8, 500);
            TextureManager.LoadTexture("PlayerRight", "Jump/PlayerRight", 8, 500);


            TextureManager.Font = Content.Load<SpriteFont>("Font");
            TextureManager.BigFont = Content.Load<SpriteFont>("BigFont");

            TextureManager.LoadSound("Drop", "Sounds/Drop");
            TextureManager.LoadSound("Jump", "Sounds/Jump");
            TextureManager.LoadSound("Level", "Sounds/Level");
            TextureManager.LoadSound("Restart", "Sounds/Restart");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            TextureManager.Update(gameTime);
            KeyboardManager.Update();
            Screen.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Screen.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
