using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonogameWindows.Cameras;
using MonogameWindows.Entities;

using MonogameWindows.EntityComponents;
using Microsoft.Xna.Framework;

namespace MonogameWindows.Controller
{
    class RenderingEngine
    {

        private FirstPersonCamera camera;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch sb;
        private BasicEffect basicEffect;
        private WorldContainer worldContainer;
        // CONSTRUCTOR
        // -----------------------------------------------
        public RenderingEngine(WorldContainer worldContainer, GraphicsDevice graphicsDevice)
        {
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
                VertexBuffer vb = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), g.GetVertexPositionColor().Length, BufferUsage.WriteOnly);
                g.SetVertexBuffer(vb);
            }
        }


        public void Draw(GameTime gameTime)
        {

            camera.Update(gameTime);

            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = camera.Projection;
            basicEffect.View = camera.View;
            basicEffect.World = Matrix.Identity;

            graphicsDevice.Clear(Color.Black);

            foreach (Entity e in worldContainer.GetEntities().Values)
            {
                e.Draw(graphicsDevice, basicEffect);
            }
        }

        // PROPERTIES
        // -----------------------------------------------
    }
}
