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
        private int degree = 0;
        private Vector3[] vertices = new Vector3[8];
        // Set of edges this node belongs to
        private Dictionary<int, Edge> edges = new Dictionary<int, Edge>();


        private float distanceToCamera = 0;
        /**
         * Nominal Attribut of Node, depicted as one of seven hues.
         **/
        public enum NodeType
        {
            CYAN = 0,
            BLUE = 1,
            PINK = 2,
            RED = 3,
            ORANGE = 4,
            BROWN = 5,
            YELLOW = 6,
            GREEN = 7
        }


        private NodeType nodeType;

        // CONSTRUCTOR
        // -----------------------------------------------

        // Default display type is 2D
        public Node(Vector3 position, int graphId, int nodeId, int nodeType, RenderState displayType = RenderState.MODEL3D, float size = 0.03f)
        {
            GRAPH_ID = graphId;
            NODE_ID = nodeId;

            // Deciding node type
            Console.WriteLine("NODE TYPE IN CONSTRUCTOR: " + nodeType);
            this.nodeType = (NodeType) nodeType;


            this.position = position;
            this.renderState = displayType;
            this.color = GetColorFromType();
            this.size = size;
            SetupGraphicsComponent();
        }

        public Node(Vector3 position, int graphId, int nodeId, Color color, RenderState displayType, float size) :
            this(position, graphId, nodeId, 0, displayType, size)
        {
            this.color = color;
            SetupGraphicsComponent();
        }

        // METHODS & FUNCTIONS
        // -----------------------------------------------

        private Color GetColorFromType()
        {
            Console.WriteLine("COLOR PICKED: " + nodeType);
            switch (nodeType)
            {
                case NodeType.CYAN:
                    return new Color(0, 238, 255, 255);
                    //return new Color(0, 0, 0, 255);

                case NodeType.BLUE:
                    return new Color(0, 47, 255, 255);

                case NodeType.PINK:
                    return new Color(217, 0, 255, 255);

                case NodeType.RED:
                    return new Color(255, 0, 106, 255);

                case NodeType.ORANGE:
                    return new Color(255, 145, 0, 255);

                case NodeType.BROWN:
                    return new Color(117, 66, 0, 255);

                case NodeType.YELLOW:
                    return new Color(255, 255, 20, 255);

                case NodeType.GREEN:
                    return new Color(21, 255, 0, 255);
         
                default:
                    return Color.Red;
            }
            
        }


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

                SpherePrimitive sphere = new SpherePrimitive(position, color, 0.1f, 16);
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

        /// <summary>
        /// Extra method for setting up the graphics component while simultaneously
        /// changing the renderstate.
        /// </summary>
        /// <param name="renderState">decides, whether a 2D or 3D Node is drawn</param>
        /// <returns></returns>
        public override Graphics SetupGraphicsComponent(RenderState renderState)
        {
            this.renderState = renderState;
            return SetupGraphicsComponent();
        }

        /// <summary>
        /// Adds an edge to the set of edges of this node.
        /// </summary>
        /// <param name="e"></param>
        public void AddEdge(Edge e)
        {
          //  edges.Add(e.GetID(), e);
            degree++;
        }


        public void RemoveEdge(Edge e)
        {
            if (edges.ContainsKey(e.GetID()))
                edges.Remove(e.GetID());
            else
                throw new ArgumentException("THE SPECIFIED EDGE " + e.GetID() + " WAS NEVER A LINK BETWEEN THE CURRENT NODE " + NODE_ID + "AND ANOTHER NODE.");
        }

        public override void UpdateDistanceToCamera(Vector3 cameraPosition)
        {
            distanceToCamera = Vector3.Distance(cameraPosition, position);
        }
        
        public override void Draw(GraphicsDevice graphicsDevice, BasicEffect effect, Effect custom)
        {
            graphicsDevice.BlendState = BlendState.AlphaBlend;
            float alpha = (10 - distanceToCamera) / 10;
            if (alpha < 0)
                alpha = 0;
            effect.Alpha = alpha;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                //custom.CurrentTechnique.Passes[0].Apply();
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

            base.Draw(graphicsDevice, effect, custom);
            graphicsDevice.BlendState = BlendState.Opaque;
        }


        /*public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.BlendState = BlendState.AlphaBlend;

            foreach (Effect effect in effects) {
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
                }
            base.Draw(graphicsDevice);
            graphicsDevice.BlendState = BlendState.Opaque;
        }*/

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
            return "NODE : " + NODE_ID + " POSITION: " + position + " TYPE: " + nodeType + " DEGREE: " + degree +  " COMPONENT OF GRAPH: " + GRAPH_ID;
        }

        public override int GetID()
        {
            return NODE_ID;
        }

        public NodeType GetNodeType()
        {
            return nodeType;
        }

        public void SetNodeType(NodeType nodeType)
        {
            this.nodeType = nodeType;
        }

        public void SetNodeType(int i)
        {
            if (i < 0 || i > 8)
                throw new ArgumentOutOfRangeException("THE NODE TYPE MUST NOT BE LESS THAN 0 OR GREATER THAN 8.");
            this.nodeType = (NodeType)i;
        }

        // PROPERTIES
        // -----------------------------------------------
    }
}
