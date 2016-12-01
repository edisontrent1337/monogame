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



    class Edge : GraphComponent
    {

        private Node source, destination;
        private List<LineSegment> linesegments = new List<LineSegment>();
        enum QUALITY_LEVEL
        {
            LOW = 256,
            MEDIUM = 128,
            HIGH = 256,
            ULTRA = 2048,
            MAX = 4096
        }

        private float thickness = 0.01f;
        private QUALITY_LEVEL levelOfQuality = QUALITY_LEVEL.LOW;
        private short segmentQuality = 2;


        private List<Vector3> points = new List<Vector3>();

        private Vector3 startPoint;
        private Vector3 endPoint;
        private Vector3 controlPoint;

        private List<VertexPositionNormalColor> vertexPositionNormalColor;

        private float length = 0;

        // CONSTRUCTOR
        // -----------------------------------------------

        public Edge(Node source, Node destination, float thickness, DisplayType displayType)
        {
            this.source = source;
            this.destination = destination;
            this.thickness = thickness;
            this.displayType = displayType;
            this.startPoint = source.GetPosition();
            this.endPoint = destination.GetPosition();
            this.displayType = displayType;
            this.color = GetRandomColor();
            SetupLinesegment();

        }

        public Edge(Node source, Node destination, float thickness, DisplayType displayType, Color color) :
            this(source, destination,thickness,displayType)
        {
            this.color = color;
            SetupLinesegment();

        }



        // METHODS & FUNCTIONS
        // -----------------------------------------------

        private void SetupLinesegment()
        {
            Vector3 controlPointAxis = CalculateMinimumDisplacementAxis(startPoint, endPoint);
            //Vector3 controlPointAxis = Vector3.UnitY;
            controlPoint = (endPoint - startPoint) / 2 + startPoint + controlPointAxis * 2;
            //controlPoint = (endPoint - startPoint) / 2 + startPoint + new Vector3(0, 3, 0);
            linesegments.Add(new LineSegment(startPoint, controlPoint, endPoint));


            Matrix rotation;
            for (float step = 0; step <= 1; step += 1f / (int)levelOfQuality)
            {
                points.Add(GetQuadraticBezierPoint(linesegments[0], step));
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                length += Vector3.Distance(points[i], points[i + 1]);
            }


            // 2D DATA
            if (displayType.Equals(DisplayType.MODEL2D))
            {

                graphics = new Graphics(this, PrimitiveType.LineStrip);
                vertexPositionNormalColor = new List<VertexPositionNormalColor>();
                for (int i = 0; i < points.Count; i++)
                {
                    vertexPositionNormalColor.Add(new VertexPositionNormalColor(points[i], Vector3.UnitY, color));
                }
                graphics.SetPrimitiveCount(points.Count - 1); // points - 1 line primitives
            }

            // 3D DATA
            else if (displayType.Equals(DisplayType.MODEL3D))
            {
                graphics = new Graphics(this, PrimitiveType.TriangleList);
                vertexPositionNormalColor = new List<VertexPositionNormalColor>();

                // Iterate over all line primitives and build tubes around them
                for (int i = 0; i < points.Count - 1; i++)
                {
                    Vector3 lineStart = points[i];
                    Vector3 lineEnd = points[i + 1];
                    Vector3 position = (lineEnd - lineStart) / 2 + lineStart;

                    Vector3 dir = lineEnd - lineStart;
                    dir.Normalize();
                    Vector3 rotationAxis = Vector3.Cross(controlPointAxis, dir);
                    //Vector3 rotationAxis = Vector3.Cross(Vector3.UnitY, dir);
                    rotationAxis.Normalize();

                    float angle = (float)Math.Acos(Vector3.Dot(controlPointAxis, dir));
                   // float angle = (float)Math.Acos(Vector3.Dot(Vector3.UnitY, dir));

                    rotation = Matrix.CreateFromAxisAngle(rotationAxis, angle);

                    float size = length / ((int)levelOfQuality - 2);

                    Vector3 cubeDimensions = Vector3.Zero;

                    if (i == 0 || i == points.Count - 2)
                        size /= 2;

                    if(controlPointAxis.X != 0)
                    {
                        cubeDimensions = new Vector3(size, thickness, thickness);
                    }

                    else if(controlPointAxis.Y != 0)
                    {
                        cubeDimensions = new Vector3(thickness, size, thickness);

                    }

                    else if(controlPointAxis.Z != 0)
                    {
                        cubeDimensions = new Vector3(thickness, thickness, size);

                    }


                    //
                    Cube cube = new Cube(position, color, cubeDimensions.X, cubeDimensions.Y , cubeDimensions.Z, rotation);
                   // Cube cube = new Cube(position, Color.Green, 0.01f, size, 0.01f, rotation);

                    foreach (VertexPositionNormalColor vpc in cube.GetVertexPositionNormalColor())
                    {
                        vertexPositionNormalColor.Add(vpc);
                    }

                }

                graphics.SetPrimitiveCount((points.Count - 1) * Cube.NUMBER_OF_PRIMITIVES * 3);

            }

            graphics.SetVertexPositionNormalColor(vertexPositionNormalColor.ToArray());
        }


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
            effect.EnableDefaultLighting();
            if (displayType.Equals(DisplayType.MODEL2D) || displayType.Equals(DisplayType.MODEL3D))
            {
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    graphicsDevice.SetVertexBuffer(graphics.VertexBuffer);
                    graphicsDevice.DrawPrimitives(graphics.GetPrimitiveType(), 0, graphics.GetPrimitiveCount());
                }
            }
            else if (displayType.Equals(DisplayType.MODEL3D))
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

        // PROPERTIES
        // -----------------------------------------------


    }
}
