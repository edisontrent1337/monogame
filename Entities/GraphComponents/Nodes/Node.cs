using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainBase.Entities.GraphComponents.Egde;
using RainBase.EntityComponents;

namespace RainBase.Entities.GraphComponents.Nodes
{
    class Node : GraphComponent
    {

        // Center position of the cube
        private Vector3 position;
        private float size = 0;

        // Set of edges this node belongs to
        private Dictionary<int, Edge> edges = new Dictionary<int, Edge>();

        // CONSTRUCTOR
        // -----------------------------------------------

        // Default display type is 2D
        public Node(Vector3 position, Color color, DisplayType displayType = DisplayType.MODEL3D, float size = 0.03f)
        {
            this.position = position;
            this.displayType = displayType;

            this.size = size;

            float x = position.X;
            float y = position.Y;
            float z = position.Z;

            VertexPositionColor[] faceData;

            Vector3[] vertices = new Vector3[8];
            Vector3[] normals = new Vector3[6];

            int numberOfPrimitives = 12;

            graphics = new Graphics(this, PrimitiveType.TriangleList);
            graphics.SetPrimitiveCount(numberOfPrimitives);

            if (displayType.Equals(DisplayType.MODEL3D))
            {
                faceData = new VertexPositionColor[numberOfPrimitives * 3];

                vertices[0] = position + new Vector3(-1, -1, -1) * size;
                vertices[1] = position + new Vector3(1, -1, -1) * size;
                vertices[2] = position + new Vector3(1, -1, 1) * size;
                vertices[3] = position + new Vector3(-1, -1, 1) * size;

                vertices[4] = position + new Vector3(-1, 1, -1) * size;
                vertices[5] = position + new Vector3(1, 1, -1) * size;
                vertices[6] = position + new Vector3(1, 1, 1) * size;
                vertices[7] = position + new Vector3(-1, 1, 1) * size;



                // BOTTOM FACE

                faceData[0] = new VertexPositionColor(vertices[0], color);
                faceData[1] = new VertexPositionColor(vertices[3], color);
                faceData[2] = new VertexPositionColor(vertices[1], color);
                faceData[3] = new VertexPositionColor(vertices[3], color);
                faceData[4] = new VertexPositionColor(vertices[2], color);
                faceData[5] = new VertexPositionColor(vertices[1], color);

                // FRONT FACE

                faceData[6] = new VertexPositionColor(vertices[4], color);
                faceData[7] = new VertexPositionColor(vertices[0], color);
                faceData[8] = new VertexPositionColor(vertices[5], color);
                faceData[9] = new VertexPositionColor(vertices[0], color);
                faceData[10] = new VertexPositionColor(vertices[1], color);
                faceData[11] = new VertexPositionColor(vertices[5], color);

                // TOP FACE

                faceData[12] = new VertexPositionColor(vertices[7], color);
                faceData[13] = new VertexPositionColor(vertices[4], color);
                faceData[14] = new VertexPositionColor(vertices[6], color);
                faceData[15] = new VertexPositionColor(vertices[4], color);
                faceData[16] = new VertexPositionColor(vertices[5], color);
                faceData[17] = new VertexPositionColor(vertices[6], color);

                // RIGHT FACE

                faceData[18] = new VertexPositionColor(vertices[5], color);
                faceData[19] = new VertexPositionColor(vertices[1], color);
                faceData[20] = new VertexPositionColor(vertices[6], color);
                faceData[21] = new VertexPositionColor(vertices[1], color);
                faceData[22] = new VertexPositionColor(vertices[2], color);
                faceData[23] = new VertexPositionColor(vertices[6], color);

                // LEFT FACE

                faceData[24] = new VertexPositionColor(vertices[7], color);
                faceData[25] = new VertexPositionColor(vertices[3], color);
                faceData[26] = new VertexPositionColor(vertices[4], color);
                faceData[27] = new VertexPositionColor(vertices[3], color);
                faceData[28] = new VertexPositionColor(vertices[0], color);
                faceData[29] = new VertexPositionColor(vertices[4], color);

                // BACK FACE

                faceData[30] = new VertexPositionColor(vertices[6], color);
                faceData[31] = new VertexPositionColor(vertices[2], color);
                faceData[32] = new VertexPositionColor(vertices[7], color);
                faceData[33] = new VertexPositionColor(vertices[2], color);
                faceData[34] = new VertexPositionColor(vertices[3], color);
                faceData[35] = new VertexPositionColor(vertices[7], color);


                graphics.SetVertexPositionColor(faceData);
            }
            else
            {

            }
        }


        // METHODS & FUNCTIONS
        // -----------------------------------------------


        public void AddEdge(Edge e)
        {
            //edges.Add(e.Get)
        }


        public void RemoveEdge(Edge e)
        {

        }


        public override void Draw(GraphicsDevice graphicsDevice, BasicEffect effect)
        {
            if (displayType.Equals(DisplayType.SPRITE2D))
            {

            }

            else if (displayType.Equals(DisplayType.MODEL3D))
            {


                foreach(EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    graphicsDevice.SetVertexBuffer(graphics.VertexBuffer);
                    graphicsDevice.DrawPrimitives(graphics.GetPrimitiveType(), 0, graphics.GetPrimitiveCount());
                }
            }

            base.Draw(graphicsDevice, effect);
        }


        public override void Update()
        {
            base.Update();
        }


        public Vector3 GetPosition()
        {
            return position;
        }


        // PROPERTIES
        // -----------------------------------------------
    }
}
