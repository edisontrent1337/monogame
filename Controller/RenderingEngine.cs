using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using RainBase.Cameras;
using RainBase.Entities;

using RainBase.EntityComponents;
using RainBase.Test;
using Microsoft.Xna.Framework;

namespace RainBase.Controller
{
    class RenderingEngine
    {

        private FirstPersonCamera camera;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch sb;
        private BasicEffect basicEffect;
        private WorldContainer worldContainer;
        private string OS;
        // CONSTRUCTOR
        // -----------------------------------------------
        public RenderingEngine(string OS, WorldContainer worldContainer, GraphicsDevice graphicsDevice)
        {
            this.OS = OS;
            this.graphicsDevice = graphicsDevice;
            this.worldContainer = worldContainer;
            this.camera = new FirstPersonCamera(new Vector3(0, 1f, 0f), Vector3.Zero, 3, graphicsDevice);
            this.sb = new SpriteBatch(graphicsDevice);
            this.basicEffect = new BasicEffect(graphicsDevice);
        }


        // METHODS & FUNCTIONS
        // -----------------------------------------------

        public void InitGraphics(Entity e)
        {
            if (e.HasGraphics())
            {
                Graphics g = e.GetGraphics();
                VertexBuffer vb = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalColor), g.GetVertexPositionNormalColor().Length, BufferUsage.WriteOnly);
                g.SetVertexBuffer(vb);
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

        // PROPERTIES
        // -----------------------------------------------
    }
}
