﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RainBase.Controller;
using RainBase.Entities.RoomComponents;
using System;

using RainBase.Entities;
using RainBase.Entities.Graphs;
using RainBase.Entities.GraphComponents;

namespace RainBase.Main


{
    public class RainWindows : Game
    {
        // CONTROLLER

        private RenderingEngine renderingEngine;
        private WorldContainer worldContainer;
        private WorldController worldController;
        private WindowsInputHandler inputHandler;

        private const short BUFFER_WIDTH = 1920;
        private const short BUFFER_HEIGHT = 1080;

        private const float SPEED = 0.3f;
        
        private Vector3 origin = new Vector3(0f, 0f, 0f);

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect basicEffect;

        KeyboardState oldState = Keyboard.GetState();

        private static string OS = "WINDOWS";

        private Effect effect;

        // CONSTRUCTOR
        // -----------------------------------------------
        public RainWindows()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BUFFER_WIDTH;
            graphics.PreferredBackBufferHeight = BUFFER_HEIGHT;
            //graphics.IsFullScreen = true;
            //graphics.PreferMultiSampling = true;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";

        }

        // METHODS & FUNCTIONS
        // -----------------------------------------------

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

       
            /**
             * Controllers can be created only here, not in Game1's constructor,
             * because GraphicsDevice has not been initialized up there yet.
             **/
            effect = Content.Load<Effect>("TransparencyShader");
            worldContainer = new WorldContainer(this);
            worldController = new WorldController(this, worldContainer);
            renderingEngine = new RenderingEngine(OS, worldContainer, GraphicsDevice,effect);
            inputHandler = new WindowsInputHandler(renderingEngine.GetCamera(), worldController, worldContainer);

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
            renderingEngine.Font = Content.Load<SpriteFont>("Arial");
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

            inputHandler.Update(gameTime);
            worldController.Update();
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
