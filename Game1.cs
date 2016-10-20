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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        //CAMERA SETUP
        Vector3 cameraDirection;
        Vector3 cameraPosition;

        Matrix projectionMatrix;
        Matrix viewMatrix;
        Matrix worldMatrix;

        VertexPositionColor[] triangle;
        VertexBuffer vertexBuffer;

        BasicEffect basicEffect;

        bool orbit = false;

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
            // TODO: Add your initialization logic here

            base.Initialize();

            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1f;
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;

            cameraDirection = new Vector3(0f, 0f, 0f);
            cameraPosition = new Vector3(0f, 0f, -100f);

            // MATRIZEN
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GraphicsDevice.DisplayMode.AspectRatio, 1f, 1000f);
            viewMatrix = Matrix.CreateLookAt(cameraPosition, cameraDirection, new Vector3(0f, 1f, 0f));
            worldMatrix = Matrix.CreateWorld(cameraDirection, Vector3.Forward, Vector3.Up);


            //TRIANGLE
            triangle = new VertexPositionColor[3];
            triangle[0] = new VertexPositionColor(new Vector3(0f, 20f, 0), Color.Red);
            triangle[1] = new VertexPositionColor(new Vector3(-20f, -20f, 0), Color.Green);
            triangle[2] = new VertexPositionColor(new Vector3(20f, -20f, 0), Color.Blue);


            //VERTEX BUFFER
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangle);

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
                cameraPosition.Y -= 1f;
                cameraDirection.Y -= 1f;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                cameraPosition.Y += 1f;
                cameraDirection.Y += 1f;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                cameraPosition.Z += 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                cameraPosition.Z -= 1f;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                orbit = !orbit;
            }

            if(orbit)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(2f));
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


            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            RasterizerState state = new RasterizerState();
            state.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = state;

            foreach(EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
            }


            base.Draw(gameTime);
        }
    }
}
