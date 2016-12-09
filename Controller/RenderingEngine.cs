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
        private string OS;

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


            if(OS == "WINDOWS")
                graphicsDevice.Clear(Color.Black);
            if(OS == "ANDROID")
                graphicsDevice.Clear(Color.Transparent);

            foreach (Entity e in worldContainer.GetEntities().Values)
            {
                if(e.HasGraphics())
                    e.Draw(graphicsDevice, basicEffect);
            }

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
