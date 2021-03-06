﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RainBase.Main;
using RainBase.Entities;

using RainBase.EntityComponents;
using RainBase.VertexType;

namespace RainBase.Entities.RoomComponents
{
    public class Floor : Entity
    {

        private Color pink = new Color(1f, 0f, 195f / 255f, 1f);

        private const int GRID_CELL_WIDTH = 1;
        private const int GRID_CELL_HEIGHT = 1;

        List<VertexPositionNormalColor> vertexList = new List<VertexPositionNormalColor>();

        private Game game;
        private Vector3 origin;

        public Floor(Vector3 origin, float width, float depth)
        {

            graphics = new Graphics(this, PrimitiveType.LineList);

            this.origin = origin;
            int columns = (int) (width / GRID_CELL_WIDTH);
            int rows = (int) (depth / GRID_CELL_HEIGHT);

            float xOrigin = origin.X;
            float zOrigin = origin.Z;

            for (int x = 0; x <= columns; ++x)
            {
                vertexList.Add(new VertexPositionNormalColor(new Vector3(xOrigin + x * GRID_CELL_WIDTH, 0f, zOrigin), Vector3.UnitZ, Color.White));
                vertexList.Add(new VertexPositionNormalColor(new Vector3(xOrigin + x * GRID_CELL_WIDTH, 0f, zOrigin + depth), Vector3.UnitZ, Color.White));
            }

            for (int z = 0; z <= rows; ++z)
            {
                vertexList.Add(new VertexPositionNormalColor(new Vector3(xOrigin, 0f , zOrigin + z * GRID_CELL_HEIGHT), Vector3.UnitZ, Color.White));
                vertexList.Add(new VertexPositionNormalColor(new Vector3(xOrigin + depth, 0f, zOrigin + z * GRID_CELL_HEIGHT), Vector3.UnitZ, Color.White));
            }

            graphics.SetVertexPositionNormalColor(vertexList.ToArray());
            graphics.SetPrimitiveCount(vertexList.Count / 2);

        }


        public void Initialize()
        {

        }


        // DRAW METHOD

        public override void Draw(GraphicsDevice graphicsDevice, BasicEffect effect, Effect custom)
        {
            graphicsDevice.BlendState = BlendState.Opaque;
            foreach(EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                effect.Alpha = 1;

                graphicsDevice.SetVertexBuffer(graphics.VertexBuffer);
                graphicsDevice.DrawPrimitives(graphics.GetPrimitiveType(), 0, graphics.GetPrimitiveCount());
            }
            graphicsDevice.BlendState = BlendState.AlphaBlend;
        }



    }
}
