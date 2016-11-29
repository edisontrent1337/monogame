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
using RainBase.Test;

namespace RainBase.Entities.GraphComponents.Egde
{



    class Edge : GraphComponent
    {



        private Node source, destination;
        private List<LineSegment> linesegments = new List<LineSegment>();
        enum QUALITY_LEVEL
        {
            LOW = 64,
            MEDIUM = 512,
            HIGH = 1024,
            ULTRA = 2048,
            MAX = 4096
        }


        private float thickness = 1f;
        private QUALITY_LEVEL levelOfQuality = QUALITY_LEVEL.LOW;
        private short segmentQuality = 2;


        private List<Vector3> points = new List<Vector3>();

        private Vector3 startPoint;
        private Vector3 endPoint;

        private List <VertexPositionNormalColor> vertexPositionNormalColor;

        private float length = 0;
        // CONSTRUCTOR
        // -----------------------------------------------

        public Edge(Node source, Node destination, DisplayType displayType = DisplayType.SPRITE2D)
        {
            this.source = source;
            this.destination = destination;
            this.displayType = displayType;

            this.startPoint = source.GetPosition();

            this.endPoint = destination.GetPosition();



            Vector3 control = (endPoint - startPoint) / 2 + startPoint + new Vector3(0, 3, 0);

            // linesegments.Add(new LineSegment(startPoint, (endPoint-startPoint) / 2 + new Vector3(0,10f,0), endPoint));
            linesegments.Add(new LineSegment(startPoint, control, endPoint));


            Vector3 direction0 = startPoint - Vector3.Zero;
            Vector3 direction1 = endPoint - Vector3.Zero;

            Vector3 axis0 = control - startPoint;
            axis0.Normalize();
            Vector3 axis1 = control - endPoint;
            axis1.Normalize();

            Vector3 axis2 = Vector3.Cross(axis1, axis0);
            axis2.Normalize();

            Matrix rotation, rotationX, rotationY,rotationZ;
            for (float step = 0; step <= 1; step += 1f / (int)levelOfQuality)
            {
                points.Add(GetQuadraticBezierPoint(linesegments[0], step));
               // points.Add(GetQuadraticBezierPoint(linesegments[0], step + (1f/(int)levelOfQuality)));
            }

            for(int i = 0; i < points.Count - 1; i++)
            {
                length += Vector3.Distance(points[i], points[i + 1]);
            }
            //Console.WriteLine(points.Count + " COUNT");
            /*points.Add(startPoint);
            points.Add(endPoint);*/
           // points.Add(Vector3.Zero);
           // points.Add(startPoint);
           // points.Add(Vector3.Zero);
           // points.Add(endPoint);
           // points.Add(direction1);
            // 2D DATA
            if (displayType.Equals(DisplayType.SPRITE2D))
            {

                graphics = new Graphics(this, PrimitiveType.LineStrip);
                vertexPositionNormalColor = new List<VertexPositionNormalColor>();
                //Console.WriteLine("SIZE OF POINTS :" +  points.Count);
                for (int i = 0; i < points.Count; i++)
                {
                    vertexPositionNormalColor.Add(new VertexPositionNormalColor(points[i], Vector3.UnitZ, Color.Red));
                }

                graphics.SetPrimitiveCount(points.Count - 1);
                //graphics.SetPrimitiveCount((points.Count - 2) / 2 );

            }

            // 3D DATA
            else
            {

                graphics = new Graphics(this, PrimitiveType.TriangleList);
                vertexPositionNormalColor = new List<VertexPositionNormalColor>();


                Vector3 yAxis = Vector3.UnitY;

                int angle = 0;
                for (int i = 0; i < points.Count - 1; i++)
                {
                    Vector3 start = points[i];
                    Vector3 end = points[i + 1];
                    Vector3 position = start;

                    //Vector3 dir = end- start;
                    Vector3 dir = end - start;
                    dir.Normalize();


                    Vector3 rotationAxis = Vector3.Cross(dir, yAxis);
                    rotationAxis.Normalize();

                    dir.Normalize();
                    float depth = Vector3.Distance(end, start);


                    //acos = MathHelper.ToRadians(acos);
                   /* Console.WriteLine(dir + " DIR");
                    Console.WriteLine(rotationAxis + " ROTATION AXIS");

                    Console.WriteLine("Y*D" + Vector3.Dot(yAxis, dir));*/

                    Vector3 final = Vector3.Cross(Vector3.UnitY, dir);
                    final.Normalize();

                    float angle0 = (float)Math.Acos(Vector3.Dot(Vector3.UnitY, dir));

                    rotation = Matrix.CreateFromAxisAngle(final, angle0);
                    //rotation = Matrix.Identity;
                    //rotationY = Matrix.CreateRotationY(angleX);
                    //rotationZ = Matrix.CreateRotationZ(angleY);

                    //rotation = rotationZ * rotationY * rotationX;

                    float size = length / ((int)levelOfQuality-1);

                    Cube cube = new Cube(position, Color.Green, 0.01f, size, 0.01f, rotation);

                    foreach (VertexPositionNormalColor vpc in cube.GetVertexPositionNormalColor())
                    {
                        vertexPositionNormalColor.Add(vpc);
                        //Console.WriteLine(vpc);

                    }

                }

                graphics.SetPrimitiveCount((points.Count-1) * Cube.NUMBER_OF_PRIMITIVES * 3);

            }

            graphics.SetVertexPositionNormalColor(vertexPositionNormalColor.ToArray());
        }




        // METHODS & FUNCTIONS
        // -----------------------------------------------
        public override void Draw(GraphicsDevice graphicsDevice, BasicEffect effect)
        {
            effect.EnableDefaultLighting();
            if(displayType.Equals(DisplayType.SPRITE2D) || displayType.Equals(DisplayType.MODEL3D))
            {
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    graphicsDevice.SetVertexBuffer(graphics.VertexBuffer);
                    graphicsDevice.DrawPrimitives(graphics.GetPrimitiveType(), 0, graphics.GetPrimitiveCount());
                }
            }
            else if(displayType.Equals(DisplayType.MODEL3D))
            {

            }
            base.Draw(graphicsDevice,effect);
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
