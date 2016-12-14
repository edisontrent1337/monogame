using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using RainBase.Cameras;
using RainBase.Entities;

using RainBase.EntityComponents;
using RainBase.VertexType;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RainBase.Controller
{
    /**
     * Class that renders all entities registered at any given time.
     * Holds a reference to the graphics device and uses a first person camera.
     **/
   public class RenderingEngine
    {

        private FirstPersonCamera camera;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch sb;
        private BasicEffect basicEffect;
        private WorldContainer worldContainer;
        private SpriteFont spriteFont;
        private string OS;

        StringBuilder builder = new StringBuilder();

        public SpriteFont Font
        {
            set { spriteFont = value; }
        }

        private Matrix tangoPositionTransform = new Matrix(
            new Vector4(-1, 0, 0, 1),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 1)
            );

        // CONSTRUCTOR
        // -----------------------------------------------
        public RenderingEngine(string OS, WorldContainer worldContainer, GraphicsDevice graphicsDevice)
        {
            this.OS = OS;
            this.worldContainer = worldContainer;
            this.graphicsDevice = graphicsDevice;
            this.camera = new FirstPersonCamera(new Vector3(5, 1.80f, 5), Vector3.UnitZ, 3, graphicsDevice);
            this.sb = new SpriteBatch(graphicsDevice);
            this.basicEffect = new BasicEffect(graphicsDevice);
        }

        // METHODS & FUNCTIONS
        // -----------------------------------------------


        /**
         * Initializes the graphics component of an entity so it can be rendered properly.
         * Assigns a vertexbuffer to the graphics component of the specified entity.
         **/
        public void InitGraphics(Entity e)
        {
            if (e.HasGraphics())
            {
                
                Graphics g = e.GetGraphics();
                VertexBuffer vb = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalColor), g.GetVertexPositionNormalColor().Length, BufferUsage.WriteOnly);
                g.SetUpVertexBufferAndData(vb);
                if (g.IndexedRendering) {
                    IndexBuffer ib = new IndexBuffer(graphicsDevice, typeof(ushort), g.GetIndices().Count, BufferUsage.WriteOnly);
                    g.SetUpIndicesAndData(ib);
                 }
 
            }
        }


        public void Draw(GameTime gameTime)
        {

            camera.Update(gameTime);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = camera.Projection;
            basicEffect.EnableDefaultLighting();
            basicEffect.View = camera.View;
            basicEffect.World = Matrix.Identity;


            if (OS == "WINDOWS")
                graphicsDevice.Clear(Color.Black);
            if(OS == "ANDROID")
                graphicsDevice.Clear(Color.Transparent);


            foreach (Entity e in worldContainer.GetEntities().Values)
            {
                if(e.HasGraphics())
                    e.Draw(graphicsDevice, basicEffect);
            }
            sb.Begin();
            {
                builder.Append("CAM POS: ").Append(camera.Position);
                sb.DrawString(spriteFont, builder.ToString(), new Vector2(50, 50), Color.White);
                builder.Length = 0;

                builder.Append("CAM VELO: ").Append(camera.GetVelocity());
                sb.DrawString(spriteFont, builder.ToString(), new Vector2(50, 70), Color.White);
                builder.Length = 0;

                builder.Append("CAM ACC: ").Append(camera.GetAcceleration());
                sb.DrawString(spriteFont, builder.ToString(), new Vector2(50, 90), Color.White);
                builder.Length = 0;

                builder.Append("CAM LOOK AT: ").Append(camera.GetLookAt());
                sb.DrawString(spriteFont, builder.ToString(), new Vector2(50, 110), Color.White);
                builder.Length = 0;

                MouseState currentMouseState = Mouse.GetState();
                int mouseX = currentMouseState.X;
                int mouseY = currentMouseState.Y;

                builder.Append("CURSOR POS: ").Append("X: " + mouseX + " Y: " +mouseY);
                    sb.DrawString(spriteFont, builder.ToString(), new Vector2(50, 130), Color.White);
                builder.Length = 0;

                Vector3 nearSource = new Vector3((float)mouseX, (float)mouseY, 0);

                builder.Append("NEAR SOURCE: ").Append(nearSource);
                sb.DrawString(spriteFont, builder.ToString(), new Vector2(50, 150), Color.White);
                builder.Length = 0;

                Vector3 farSource = new Vector3((float)mouseX, (float)mouseY, 1f);


                builder.Append("FAR SOURCE ").Append(farSource);
                sb.DrawString(spriteFont, builder.ToString(), new Vector2(50, 170), Color.White);
                builder.Length = 0;


                Matrix world = Matrix.CreateTranslation(0, 0, 0);

                Vector3 nearPoint = camera.GetViewport().Unproject(nearSource, camera.Projection, camera.View, world);

                builder.Append("NEAR POINT ").Append(nearPoint);
                sb.DrawString(spriteFont, builder.ToString(), new Vector2(50, 190), Color.White);
                builder.Length = 0;

                Vector3 farPoint = camera.GetViewport().Unproject(farSource, camera.Projection, camera.View, world);

                builder.Append("FAR POINT").Append(farPoint);
                sb.DrawString(spriteFont, builder.ToString(), new Vector2(50, 210), Color.White);
                builder.Length = 0;


                Vector3 dir = farPoint - nearPoint;
                dir.Normalize();

                builder.Append("DIR ").Append(dir);
                sb.DrawString(spriteFont, builder.ToString(), new Vector2(50, 230), Color.White);
                builder.Length = 0;

                Vector2 originX = spriteFont.MeasureString("X") / 2;
                sb.DrawString(spriteFont, "X", new Vector2(640, 360), Color.White, 0, originX, 1.0f,SpriteEffects.None,0.5f);
                builder.Length = 0;

            }
            sb.End();
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;

        }


        public void SetProjectionMatrix(Matrix projectionMatrix)
        {
            camera.Projection = projectionMatrix;
        }

        public void Draw(GameTime gameTime, Vector3 pos, Quaternion q)
        {

            camera.Update(Vector3.Transform(pos, tangoPositionTransform), q);

            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = camera.Projection;
            basicEffect.EnableDefaultLighting();
            basicEffect.View = camera.View;
            basicEffect.World = Matrix.Identity;


            if (OS == "ANDROID")
                graphicsDevice.Clear(Color.Transparent);


            foreach (Entity e in worldContainer.GetEntities().Values)
            {
                if (e.HasGraphics()) 
                    e.Draw(graphicsDevice, basicEffect);
            }

        }


        // PROPERTIES
        // -----------------------------------------------

        public Vector3 GetCameraLookAt()
        {
            return camera.GetLookAt();
        }

        public FirstPersonCamera GetCamera()
        {
            return camera;
        }
    }
}
