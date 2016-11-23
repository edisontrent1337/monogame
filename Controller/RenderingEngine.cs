﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonogameWindows.Cameras;
using MonogameWindows.Models;

using MonogameWindows.ModelComponents;

namespace MonogameWindows.Controller
{
    class RenderingEngine
    {

        private Camera camera;
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
            this.camera = new PerspectiveCamera();
            this.sb = new SpriteBatch(graphicsDevice);
            this.basicEffect = new BasicEffect(graphicsDevice);
        }


        // METHODS & FUNCTIONS
        // -----------------------------------------------

        public void InitVertexBuffers(Graphics g)
        {
            VertexBuffer vb = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor),g.GetVertexPositionColor().Length, BufferUsage.WriteOnly);
            g.SetVertexBuffer(vb);

        }


        public void Draw()
        {

        }

        // PROPERTIES
        // -----------------------------------------------
    }
}