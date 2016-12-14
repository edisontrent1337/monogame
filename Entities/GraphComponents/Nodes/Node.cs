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
using RainBase.VertexType;

namespace RainBase.Entities.GraphComponents.Nodes
{
    public class Node : GraphComponent
    {
        private readonly int GRAPH_ID;
        private readonly int NODE_ID;
        // Center position of the cube
        private Vector3 position;
        private float size = 0;
        private Vector3[] vertices = new Vector3[8];
        // Set of edges this node belongs to
        private Dictionary<int, Edge> edges = new Dictionary<int, Edge>();

        public enum NodeType
        {
            CUBE,
            BALL
        }


        // CONSTRUCTOR
        // -----------------------------------------------

        // Default display type is 2D
        public Node(Vector3 position, int graphId, int nodeId, RenderState displayType = RenderState.MODEL3D, float size = 0.03f)
        {
            GRAPH_ID = graphId;
            NODE_ID = nodeId;
            this.position = position;
            this.renderState = displayType;
            this.color = GetRandomColor();
            this.size = size;
            SetupGraphicsComponent();
        }

        public Node(Vector3 position, int graphId, int nodeId, Color color, RenderState displayType, float size) :
            this(position, graphId, nodeId, displayType, size)
        {
            this.color = color;
            SetupGraphicsComponent();
        }

        // METHODS & FUNCTIONS
        // -----------------------------------------------

        /// <summary>
        /// Sets up the graphics component for this node.
        /// Distinguishes between a 2D and a 3D case.
        /// </summary>
        public override Graphics SetupGraphicsComponent()
        {
            boundingBoxes.Clear();
            if (renderState.Equals(RenderState.MODEL3D))
            {
                graphics = new Graphics(this, PrimitiveType.TriangleList);

                SpherePrimitive sphere = new SpherePrimitive(position, color, 0.05f, 16);
                boundingBoxes.Add(sphere.GetBoundingBox());
                //Console.WriteLine("NODE:" + NODE_ID + " BB: " + boundingBox.ToString());
                graphics.SetPrimitiveCount(sphere.GetPrimitiveCount());
                graphics.IndexedRendering = true;
                graphics.SetVertexPositionNormalColor(sphere.GetVertexPositionNormalColor().ToArray());
                graphics.SetIndices(sphere.GetIndices());

            }
            else
            {
                graphics = new Graphics(this, PrimitiveType.TriangleList);
                graphics.IndexedRendering = false;
                CubePrimitive cube = new CubePrimitive(position, color, 0.02f);
                boundingBoxes.Add(cube.GetBoundingBox());
                //Console.WriteLine("NODE:" + NODE_ID + " BB: " + boundingBox.ToString());
                graphics.SetPrimitiveCount(CubePrimitive.NUMBER_OF_PRIMITIVES);
                graphics.SetVertexPositionNormalColor(cube.GetVertexPositionNormalColor());

            }

            return graphics;
        }

        public override Graphics SetupGraphicsComponent(RenderState displayType)
        {
            this.renderState = displayType;
            return SetupGraphicsComponent();
        }

        /// <summary>
        /// Adds an edge to the set of edges of this node.
        /// </summary>
        /// <param name="e"></param>
        public void AddEdge(Edge e)
        {
          //  edges.Add(e.GetID(), e);
        }


        public void RemoveEdge(Edge e)
        {
            if (edges.ContainsKey(e.GetID()))
                edges.Remove(e.GetID());
            else
                throw new ArgumentException("THE SPECIFIED EDGE " + e.GetID() + " WAS NEVER A LINK BETWEEN THE CURRENT NODE " + NODE_ID + "AND ANOTHER NODE.");
        }


        public override void Draw(GraphicsDevice graphicsDevice, BasicEffect effect)
        {

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.SetVertexBuffer(graphics.VertexBuffer);
                if (graphics.IndexedRendering)
                {
                    graphicsDevice.Indices = graphics.IndexBuffer;
                    graphicsDevice.DrawIndexedPrimitives(graphics.GetPrimitiveType(), 0, 0, graphics.GetPrimitiveCount() * 2);
                }
                else
                {

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

        public override string ToString()
        {
            return "NODE : " + NODE_ID + " POSITION: " + position + " COMPONENT OF GRAPH: " + GRAPH_ID;
        }

        public override int GetID()
        {
            return NODE_ID;
        }


        // PROPERTIES
        // -----------------------------------------------
    }
}
