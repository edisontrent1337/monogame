using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameWindows
{
    class Grid
    {

        private Color pink = new Color(1f, 0f, 195f / 255f, 1f);

        private const int GRID_CELL_WIDTH = 1;
        private const int GRID_CELL_HEIGHT = 1;

        List<VertexPositionColor> vertices;

        public VertexBuffer vertexBuffer { get; set;}

        private Game game;

        public Grid(Game1.WorldDimensions dimensions, Game1 game)
        {
            int columns = dimensions.width / GRID_CELL_WIDTH;
            int rows = dimensions.depth / GRID_CELL_HEIGHT;

            Vector3 position = dimensions.origin;

            this.vertices = new List<VertexPositionColor>();


            float xOrigin = position.X - columns / 2f;
            float yOrigin = position.Y;
            float zOrigin = position.Z - rows / 2f;

            this.game = game;

            for(int x = 0; x <= columns; ++x)
            {
                vertices.Add(new VertexPositionColor(new Vector3(xOrigin + x * GRID_CELL_WIDTH, yOrigin, zOrigin), pink));
                vertices.Add(new VertexPositionColor(new Vector3(xOrigin + x * GRID_CELL_WIDTH, yOrigin, zOrigin + dimensions.depth), pink));
            }

            for(int z = 0; z <= rows; ++z)
            {
                vertices.Add(new VertexPositionColor(new Vector3(xOrigin,                yOrigin, zOrigin + z * GRID_CELL_HEIGHT), pink));
                vertices.Add(new VertexPositionColor(new Vector3(xOrigin  + dimensions.depth, yOrigin, zOrigin + z * GRID_CELL_HEIGHT), pink));
            }

            /*vertices.Add(new VertexPositionColor(new Vector3(0, 20, 0), Color.White));
            vertices.Add(new VertexPositionColor(new Vector3(-20, 20, 0), Color.White));
            vertices.Add(new VertexPositionColor(new Vector3(20, -20, 0), Color.White));*/
            vertexBuffer = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionColor), vertices.Count, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertices.ToArray());
        }



    }
}
