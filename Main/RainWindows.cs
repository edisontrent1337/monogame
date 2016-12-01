using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RainBase.Controller;
using RainBase.Entities.Room;
using System;

using RainBase.Entities;

namespace RainBase.Main


{
    public class RainWindows : Game
    {
        // CONTROLLER

        private RenderingEngine renderingEngine;
        private WorldContainer worldContainer;
        private WorldController worldController;

        private const short BUFFER_WIDTH = 1280;
        private const short BUFFER_HEIGHT = 720;

        private const float SPEED = 0.3f;
        
        private Vector3 origin = new Vector3(0f, 0f, 0f);

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect basicEffect;

        private static string OS = "WINDOWS";

        // CONSTRUCTOR
        // -----------------------------------------------
        public RainWindows()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BUFFER_WIDTH;
            graphics.PreferredBackBufferHeight = BUFFER_HEIGHT;
            graphics.PreferMultiSampling = true;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";



        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 


        // METHODS & FUNCTIONS
        // -----------------------------------------------
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

       
            /**
             * Controllers can be created only here, not in Game1's constructor,
             * because GraphicsDevice has not been initialized up there yet.
             **/
            worldContainer = new WorldContainer();
            worldController = new WorldController(this, worldContainer);
            renderingEngine = new RenderingEngine(OS, worldContainer);

            renderingEngine.SetGraphicsDevice(GraphicsDevice);

            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1f;
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;


            foreach (Entity e in worldContainer.GetEntityList())
            {
                renderingEngine.InitGraphics(e);
            }
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

            // TODO: use this.Content to load your game content here
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            renderingEngine.Draw(gameTime);

            base.Draw(gameTime);
        }


    }
}
