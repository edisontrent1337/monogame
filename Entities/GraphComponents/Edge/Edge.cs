using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RainBase.Entities.GraphComponents.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainBase.EntityComponents;
using RainBase.Entities.Primitives;
using RainBase.VertexType;

namespace RainBase.Entities.GraphComponents.Egde
{



    public class Edge : GraphComponent
    {

        private Node source, destination;
        private readonly int GRAPH_ID;
        private readonly int EDGE_ID;
        private List<LineSegment> linesegments = new List<LineSegment>();
        public enum Smoothness
        {
            NONE = 1,
            LOW = 128,
            MEDIUM = 256,
            HIGH = 512,
            ULTRA = 1024,
            MAX = 2048
        }

        private float thickness = 0.008f;
        private int levelOfSmoothness = (int) Smoothness.LOW;


        private List<Vector3> points = new List<Vector3>();

        private Vector3 startPoint;
        private Vector3 endPoint;
        private Vector3 controlPoint;

        private List<VertexPositionNormalColor> vertexPositionNormalColor;

        private float length = 0;

        // CONSTRUCTOR
        // -----------------------------------------------

        public Edge(Node source, Node destination, int graphID, int edgeID, RenderState renderState)
        {
            this.source = source;
            this.destination = destination;

            this.GRAPH_ID = graphID;
            this.EDGE_ID = edgeID;

            this.renderState = renderState;
            this.startPoint = source.GetPosition();
            this.endPoint = destination.GetPosition();
            this.renderState = renderState;
            this.color = GetRandomColor();

            SetupGraphicsComponent();
            ConnectToNodes();

        }

        public Edge(Node source, Node destination, int graphID, int edgeID, RenderState renderState, Color color) :
            this(source, destination,graphID, edgeID,renderState)
        {
            this.color = color;
            SetupGraphicsComponent();
            ConnectToNodes();

        }



        // METHODS & FUNCTIONS
        // -----------------------------------------------
        
        /// <summary>
        /// Adds this edge as a reference to the source and destination nodes.
        /// </summary>
        private void ConnectToNodes()
        {
            source.AddEdge(this);
            destination.AddEdge(this);
        }

        /// <summary>
        /// Removes this link between its source and destination nodes.
        /// </summary>
        public void DisconnectFromNodes()
        {
            source.RemoveEdge(this);
            destination.RemoveEdge(this);
        }

        /// <summary>
        /// Sets up the graphics component for this Graph component.
        /// Initializes vertex buffers for 2D or 3D rendering according to the render state.
        /// </summary>
        /// <returns> the graphics component for further setup</returns>
        public override Graphics SetupGraphicsComponent()
        {
            Vector3 controlPointAxis = CalculateMinimumDisplacementAxis(startPoint, endPoint);
            controlPoint = (endPoint - startPoint) / 2 + startPoint + controlPointAxis * 2;

            linesegments.Clear();
            points.Clear();
            boundingBoxes.Clear();
            vertexPositionNormalColor = new List<VertexPositionNormalColor>();
            length = 0;

            linesegments.Add(new LineSegment(startPoint, controlPoint, endPoint));

            Matrix rotation;
            for (float step = 0; step <= 1; step += 1f / (int)levelOfSmoothness)
            {
                points.Add(GetQuadraticBezierPoint(linesegments[0], step));
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                length += Vector3.Distance(points[i], points[i + 1]);
            }


            // 2D DATA
            if (renderState.Equals(RenderState.MODEL2D))
            {
                boundingBoxes.Clear();
                graphics = new Graphics(this, PrimitiveType.LineStrip);
                vertexPositionNormalColor = new List<VertexPositionNormalColor>();
                for (int i = 0; i < points.Count - 1; i+=2)
                {
                    GenerateHitbox(i, controlPointAxis);
                    vertexPositionNormalColor.Add(new VertexPositionNormalColor(points[i], Vector3.UnitY, color));
                    vertexPositionNormalColor.Add(new VertexPositionNormalColor(points[i + 1], Vector3.UnitY, color));
                }

                vertexPositionNormalColor.Add(new VertexPositionNormalColor(points[points.Count - 1], Vector3.UnitY, color));
                graphics.SetPrimitiveCount(points.Count - 1); // points - 1 line primitives

            }

            // 3D DATA
            else if (renderState.Equals(RenderState.MODEL3D))
            {

                graphics = new Graphics(this, PrimitiveType.TriangleList);
                vertexPositionNormalColor = new List<VertexPositionNormalColor>();
                boundingBoxes.Clear();
                // Iterate over all line primitives and build tubes around them
                for (int i = 0; i < points.Count - 1 ; i++)
                {

                    CubePrimitive cube = GenerateHitbox(i, controlPointAxis);
                    foreach (VertexPositionNormalColor vpc in cube.GetVertexPositionNormalColor())
                    {
                        vertexPositionNormalColor.Add(vpc);
                    }

                }

                graphics.SetPrimitiveCount((points.Count - 1) * CubePrimitive.NUMBER_OF_PRIMITIVES * 3);

            }

            graphics.SetVertexPositionNormalColor(vertexPositionNormalColor.ToArray());
            return graphics;
        }

        private CubePrimitive GenerateHitbox(int i, Vector3 controlPointAxis)
        {
            Vector3 lineStart = points[i];
            Vector3 lineEnd = points[i + 1];
            Vector3 position = lineStart;
            position = (lineEnd - lineStart) / 2 + lineStart;

            Vector3 dir = lineEnd - lineStart;
            dir.Normalize();
            Vector3 rotationAxis = Vector3.Cross(controlPointAxis, dir);
            rotationAxis.Normalize();

            float angle = (float)Math.Acos(Vector3.Dot(controlPointAxis, dir));

            Matrix rotation = Matrix.CreateFromAxisAngle(rotationAxis, angle);

            float size = Vector3.Distance(lineEnd, lineStart) * 0.5f;

            Vector3 cubeDimensions = Vector3.Zero;

            //if (i == 0 || i == points.Count - 1)
            //  size /= 2;

            if (controlPointAxis.X != 0)
            {
                cubeDimensions = new Vector3(size, thickness, thickness);
            }

            else if (controlPointAxis.Y != 0)
            {
                cubeDimensions = new Vector3(thickness, size, thickness);

            }

            else if (controlPointAxis.Z != 0)
            {
                cubeDimensions = new Vector3(thickness, thickness, size);

            }


            CubePrimitive cube = new CubePrimitive(position, color, cubeDimensions.X, cubeDimensions.Y, cubeDimensions.Z, rotation);
            boundingBoxes.Add(cube.GetBoundingBox());
            return cube;
        }

        public override Graphics SetupGraphicsComponent(RenderState displayType)
        {
            this.renderState = displayType;
            return SetupGraphicsComponent();
        }


        /// <summary>
        /// Calculates the axis on which the displacement between the two nodes of
        /// the egdes is the smallest, e.g. the minimum of each coordinate pairs of the
        /// positions of the nodes.
        /// </summary>
        /// <param name="v1">position of node A</param>
        /// <param name="v2">position of node B</param>
        /// <returns> minimum displacement axis</returns>
        private Vector3 CalculateMinimumDisplacementAxis(Vector3 v1, Vector3 v2)
        {
            float x = Math.Abs(v2.X - v1.X);
            float y = Math.Abs(v2.Y - v1.Y);
            float z = Math.Abs(v2.Z - v1.Z);

            float min = Math.Min(x, Math.Min(y, z));

            if (min == x)
                return Vector3.UnitX;
            if (min == y)
                return Vector3.UnitY;
            else
                return Vector3.UnitZ;

        }


        public override void Draw(GraphicsDevice graphicsDevice, BasicEffect effect)
        {
            //effect.EnableDefaultLighting();
            if (renderState.Equals(RenderState.MODEL2D) || renderState.Equals(RenderState.MODEL3D))
            {
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    graphicsDevice.SetVertexBuffer(graphics.VertexBuffer);
                    graphicsDevice.DrawPrimitives(graphics.GetPrimitiveType(), 0, graphics.GetPrimitiveCount());
                }
            }
            else if (renderState.Equals(RenderState.MODEL3D))
            {

            }
            base.Draw(graphicsDevice, effect);
        }

        public override void Update()
        {
            base.Update();
        }



        public Node GetSource()
        {
            return source;
        }

        public Node GetDestination()
        {
            return destination;
        }


        private Vector3 GetQuadraticBezierPoint(LineSegment l, float t)
        {

            Vector3 start = l.Start;
            Vector3 control = l.Control;
            Vector3 end = l.End;

            float x = (1 - t) * (1 - t) * start.X + (2 - 2 * t) * t * control.X + t * t * end.X;
            float y = (1 - t) * (1 - t) * start.Y + (2 - 2 * t) * t * control.Y + t * t * end.Y;
            float z = (1 - t) * (1 - t) * start.Z + (2 - 2 * t) * t * control.Z + t * t * end.Z;

            return new Vector3(x, y, z);
        }


        public override string ToString()
        {
            return "EDGE ID: " + EDGE_ID + " FROM NODE " + source.GetID() + " TO NODE " + destination.GetID() + " COMPONENT OF GRAPH: " + GRAPH_ID;
        }

        public override int GetID()
        {
            return EDGE_ID;
        }

        public void SetLevelOfSmoothness(int levelOfSmoothness)
        {
            this.levelOfSmoothness = levelOfSmoothness;
        }

        public int GetLevelOfSmoothness()
        {
            return levelOfSmoothness;
        }
        // PROPERTIES
        // -----------------------------------------------


    }
}
