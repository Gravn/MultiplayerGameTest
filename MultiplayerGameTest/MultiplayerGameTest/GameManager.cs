using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MultiplayerGameTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameManager : Game
    {

        private static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        public static Texture2D spr_rock, spr_ship, spr_particle;
        public static SpriteFont testfont;
        public static Vector2 cameraPosition = new Vector2(0, 0);
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameManager()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Window.Position = new Point(0, 0);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            Window.IsBorderless = true;
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
            // TODO: Add your initialization logic here
            base.Initialize();

            gameObjects.Add(new PlayerShip(new Vector2(900-32,450-32), new Vector2(0,0), new Vector2(0,0), spr_ship));
            gameObjects.Add(new Rock(new Vector2(200, 200), Vector2.Zero, Vector2.Zero, spr_rock));

            for (int i = 0; i < 36; i++)
            {
                gameObjects.Add(new Rock(new Vector2((float)Math.Sin(i) * 800f, (float)Math.Cos(i) * 2000), Vector2.Zero, Vector2.Zero, spr_rock));
            }

            for (int i = 0; i < 36; i++)
            {
                gameObjects.Add(new Rock(new Vector2((float)Math.Sin(i) * 2000f, (float)Math.Cos(i) * 800), Vector2.Zero, Vector2.Zero, spr_rock));
            }


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spr_particle = Content.Load<Texture2D>("particle");
            spr_rock = Content.Load<Texture2D>("rock");
            spr_ship = Content.Load<Texture2D>("spaceship");
            testfont = Content.Load<SpriteFont>("testFont");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            {
                Exit();
            }
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                        
            for(int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(deltaTime);
            }

                // TODO: Add your update logic here
                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(spriteBatch,deltaTime);
            }

            spriteBatch.DrawString(testfont, "halloej", new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(testfont, "Test", new Vector2(100-cameraPosition.X,120-cameraPosition.Y), Color.White, 27, Vector2.Zero, 2f, SpriteEffects.None, 1);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
