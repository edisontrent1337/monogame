using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonogameWindows.Main;
using MonogameWindows.Models;

using MonogameWindows.ModelComponents;

namespace MonogameWindows.Models.Room
{
    class Floor : Entity
    {

        private Color pink = new Color(1f, 0f, 195f / 255f, 1f);
     
        private const int GRID_CELL_WIDTH = 1;
        private const int GRID_CELL_HEIGHT = 1;

        List<VertexPositionColor> vertexList;

        private Game game;
        private Vector3 center;



        public Floor(Game1.WorldDimensions dimensions, Game1 game)
        {
            int columns = dimensions.width / GRID_CELL_WIDTH;
            int rows = dimensions.depth / GRID_CELL_HEIGHT;

            graphics = new Graphics();

            Vector3 position = dimensions.origin;

            this.vertexList = new List<VertexPositionColor>();

            float xOrigin = position.X;
            float yOrigin = position.Y;
            float zOrigin = position.Z;

            this.game = game;

            for(int x = 0; x <= columns; ++x)
            {
                vertexList.Add(new VertexPositionColor(new Vector3(xOrigin + x * GRID_CELL_WIDTH, yOrigin, zOrigin), Color.White));
                vertexList.Add(new VertexPositionColor(new Vector3(xOrigin + x * GRID_CELL_WIDTH, yOrigin, zOrigin + dimensions.depth), Color.White));
            }

            for(int z = 0; z <= rows; ++z)
            {
                vertexList.Add(new VertexPositionColor(new Vector3(xOrigin,                yOrigin, zOrigin + z * GRID_CELL_HEIGHT), Color.White));
                vertexList.Add(new VertexPositionColor(new Vector3(xOrigin  + dimensions.depth, yOrigin, zOrigin + z * GRID_CELL_HEIGHT), Color.White));
            }


            graphics.SetVertexPositionColor(vertexList.ToArray());
            graphics.SetPrimitiveCount(vertexList.Count / 2);

            graphics.VertexBuffer = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionColor), vertexList.Count, BufferUsage.WriteOnly);
            graphics.VertexBuffer.SetData<VertexPositionColor>(vertexList.ToArray());
        }


        public Floor(Vector3 origin, float width, float depth)
        {
            int columns = (int) (width / GRID_CELL_WIDTH);
            int rows = (int) (depth / GRID_CELL_HEIGHT);

            float xOrigin = origin.X;
            float zOrigin = origin.Z;

            for (int x = 0; x <= columns; ++x)
            {
                vertexList.Add(new VertexPositionColor(new Vector3(xOrigin + x * GRID_CELL_WIDTH, 0f, zOrigin), Color.White));
                vertexList.Add(new VertexPositionColor(new Vector3(xOrigin + x * GRID_CELL_WIDTH, 0f, zOrigin + depth), Color.White));
            }

            for (int z = 0; z <= rows; ++z)
            {
                vertexList.Add(new VertexPositionColor(new Vector3(xOrigin, 0f , zOrigin + z * GRID_CELL_HEIGHT), Color.White));
                vertexList.Add(new VertexPositionColor(new Vector3(xOrigin + depth, 0f, zOrigin + z * GRID_CELL_HEIGHT), Color.White));
            }

        }


        public void Initialize()
        {

        }


        // DRAW METHOD

        public void Draw(FirstPersonCamera camera, BasicEffect effect)
        {
            effect.VertexColorEnabled = true;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.World = Matrix.Identity;


            foreach(EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                game.GraphicsDevice.SetVertexBuffer(graphics.VertexBuffer);
                game.GraphicsDevice.DrawPrimitives(PrimitiveType.LineList, 0, graphics.VertexBuffer.VertexCount / 2);
            }
        }

        

    }
}
