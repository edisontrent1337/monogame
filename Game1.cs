using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameWindows
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {



        private const short WORLD_WIDTH = 128;
        private const short WORLD_HEIGHT = 128;
        private const short WORLD_DEPTH = 128;

        private const short BUFFER_WIDTH = 1280;
        private const short BUFFER_HEIGHT = 720;

        private const float SPEED = 0.3f;

        public struct WorldDimensions {
            public int width
            {
                get; private set;
            }

            public int height
            {
                get; private set;
            }
            public int depth
            {
                get; private set;
            }

            public Vector3 origin
            {
                get; private set;
            }

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
        Vector3 cameraDirection;
        Vector3 cameraPosition;

        Matrix projectionMatrix;
        Matrix viewMatrix;
        Matrix worldMatrix;

        VertexPositionColor[] triangle;
        VertexPositionColor[] floorTile;
        VertexBuffer vertexBuffer;
        VertexBuffer floorBuffer;

        BasicEffect basicEffect;
        Grid grid;

        bool orbit = false;

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
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            worldDimensions = new WorldDimensions(WORLD_WIDTH, WORLD_WIDTH,WORLD_DEPTH,origin);

            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1f;
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;

            cameraDirection = new Vector3(0f, 1f, 0f);
            cameraPosition = new Vector3(0f, 5f, -64f);

            // MATRIZEN
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GraphicsDevice.DisplayMode.AspectRatio, 1f, 32f);
            viewMatrix = Matrix.CreateLookAt(cameraPosition, cameraDirection, new Vector3(0f, 1f, 0f));
            worldMatrix = Matrix.CreateWorld(cameraDirection, Vector3.Forward, Vector3.Up);

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
            triangle[0] = new VertexPositionColor(new Vector3(-64f, 0f, -64f), Color.Red);
            triangle[1] = new VertexPositionColor(new Vector3(64f, 0f, -64f), Color.Green);
            triangle[2] = new VertexPositionColor(new Vector3(-64f, 0f, 64f), Color.Blue);


            triangle[3] = new VertexPositionColor(new Vector3(64f, 0f, -64f), Color.Green);
            triangle[4] = new VertexPositionColor(new Vector3(64f, 0f, 64f), Color.Red);
            triangle[5] = new VertexPositionColor(new Vector3(-64f, 0f, 64f), Color.Blue);

            grid = new Grid(worldDimensions,this);


            //VERTEX BUFFER
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 6, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangle);

            floorBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 6, BufferUsage.WriteOnly);
            floorBuffer.SetData<VertexPositionColor>(floorTile);


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

            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                cameraPosition.X -= 1f;
                cameraDirection.X -= 1f;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                cameraPosition.X += 1f;
                cameraDirection.X += 1f;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                cameraPosition.Z -= SPEED;
                cameraDirection.Z -= SPEED;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                cameraPosition.Z += SPEED;
                cameraDirection.Z += SPEED;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
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

            viewMatrix = Matrix.CreateLookAt(cameraPosition, cameraDirection, Vector3.Up);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            basicEffect.Projection = projectionMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.World = worldMatrix;


            GraphicsDevice.Clear(Color.Purple);
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            RasterizerState state = new RasterizerState();
            state.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = state;

            foreach(EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
            }


            GraphicsDevice.SetVertexBuffer(grid.vertexBuffer);

            foreach(EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.LineList, 0, grid.vertexBuffer.VertexCount / 2);
            }


            base.Draw(gameTime);
        }


    }
}
