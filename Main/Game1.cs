using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameWindows.Controller;
using MonogameWindows.Models.Room;
using System;

using MonogameWindows.Models;

namespace MonogameWindows.Main




{
    public class Game1 : Game
    {


        // CONTROLLER


        private RenderingEngine renderingEngine;
        private WorldContainer worldContainer;
        private WorldController worldController;


        private const short WORLD_WIDTH = 128;
        private const short WORLD_HEIGHT = 128;
        private const short WORLD_DEPTH = 128;

        private const short BUFFER_WIDTH = 1280;
        private const short BUFFER_HEIGHT = 720;

        private const float SPEED = 0.3f;

        public struct WorldDimensions {
            public int width, height, depth;
            public Vector3 origin;

            public WorldDimensions(int width, int height, int depth, Vector3 origin)
            {
                this.width = width;
                this.height = height;
                this.depth = depth;
                this.origin = origin;
            }


        }

        private WorldDimensions _worldDimensions;
        public WorldDimensions worldDimensions
        {
            get
            {
                return _worldDimensions;
            }
            set
            {
                _worldDimensions = value; 
            }
        }


        private Vector3 origin = new Vector3(0f, 0f, 0f);

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

     

        //CAMERA SETUP


        VertexPositionColor[] triangle;
        VertexPositionColor[] floorTile;
        VertexBuffer vertexBuffer;
        VertexBuffer floorBuffer;

        BasicEffect basicEffect;
        Floor grid;

        bool orbit = false;

        // CONSTRUCTOR
        // -----------------------------------------------
        public Game1()
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

            base.Initialize();

            /**
             * Controllers can be created only here, not in Game1's constructor,
             * because GraphicsDevice has not been initialized up there yet.
             **/
            worldContainer = new WorldContainer();
            worldController = new WorldController(this, worldContainer);
            renderingEngine = new RenderingEngine(worldContainer, GraphicsDevice);


            worldDimensions = new WorldDimensions(WORLD_WIDTH, WORLD_WIDTH,WORLD_DEPTH,origin);

            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1f;
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;

          


            //FLOOR TILE
            floorTile = new VertexPositionColor[6];
            floorTile[0].Position = new Vector3(-20, -20,0);
            floorTile[1].Position = new Vector3(-20, 20,0);
            floorTile[2].Position = new Vector3(20, -20,0);
            floorTile[3].Position = floorTile[1].Position;
            floorTile[4].Position = new Vector3(20, 20,0);
            floorTile[5].Position = floorTile[2].Position;

            //TRIANGLE
            triangle = new VertexPositionColor[6];
            triangle[0] = new VertexPositionColor(new Vector3(-WORLD_WIDTH, 0f, -WORLD_DEPTH), Color.Red);
            triangle[1] = new VertexPositionColor(new Vector3(WORLD_WIDTH, 0f, -WORLD_DEPTH), Color.Green);
            triangle[2] = new VertexPositionColor(new Vector3(-WORLD_WIDTH, 0f, WORLD_DEPTH), Color.Blue);


            triangle[3] = new VertexPositionColor(new Vector3(WORLD_WIDTH, 0f, -WORLD_DEPTH), Color.Green);
            triangle[4] = new VertexPositionColor(new Vector3(WORLD_WIDTH, 0f, WORLD_DEPTH), Color.Red);
            triangle[5] = new VertexPositionColor(new Vector3(-WORLD_WIDTH, 0f, WORLD_DEPTH), Color.Blue);

           // grid = new Floor(worldDimensions,this);


            //VERTEX BUFFER
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 6, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangle);

            floorBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 6, BufferUsage.WriteOnly);
            floorBuffer.SetData<VertexPositionColor>(floorTile);

            foreach (Entity e in worldContainer.GetEntities())
            {
                renderingEngine.InitGraphics(e);
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

            // TODO: Add your update logic here

            /*if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                cameraPosition.Z += 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                cameraPosition.Z -= 1f;
            }

            if(Mouse.GetState().LeftButton.Equals(ButtonState.Pressed)) {

                orbit = !orbit;
            }

            if(orbit)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(0.25f));
                cameraPosition = Vector3.Transform(cameraPosition, rotationMatrix);

            }

            viewMatrix = Matrix.CreateLookAt(cameraPosition, cameraDirection, Vector3.Up);*/

            //firstPersonCamera.Update(gameTime);
            //Console.WriteLine(firstPersonCamera.Position);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            renderingEngine.Draw(gameTime);
            /*basicEffect.Projection = firstPersonCamera.Projection;
            basicEffect.View = firstPersonCamera.View;
            basicEffect.World = Matrix.Identity;


            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            RasterizerState state = new RasterizerState();
            state.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = state;*/

           /* foreach(EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
            }*/

            //grid.Draw(firstPersonCamera, basicEffect);

            base.Draw(gameTime);
        }


    }
}
