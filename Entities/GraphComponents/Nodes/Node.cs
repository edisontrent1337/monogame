using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainBase.Entities.GraphComponents.Egde;
using RainBase.EntityComponents;
using RainBase.Entities.Primitives;

namespace RainBase.Entities.GraphComponents.Nodes
{
    class Node : GraphComponent
    {

        // Center position of the cube
        private Vector3 position;
        private float size = 0;

        private VertexPositionColor[] faceData;
        private Vector3[] vertices = new Vector3[8];
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

            int numberOfPrimitives = 12;

            graphics = new Graphics(this, PrimitiveType.TriangleList);
            graphics.SetPrimitiveCount(numberOfPrimitives);

            if (displayType.Equals(DisplayType.MODEL3D))
            {
                Cube cube = new Cube(position, Color.White,0.03f);
                graphics.SetVertexPositionNormalColor(cube.GetVertexPositionNormalColor());
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
