using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RainBase.Entities.GraphComponents.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainBase.EntityComponents;
namespace RainBase.Entities.GraphComponents.Egde
{
    class Edge : GraphComponent
    {



        private Node source, destination;
        private List<LineSegment> linesegments = new List<LineSegment>();

        private float thickness = 1f;
        private float quality = 1/128f;
        private short segmentQuality = 2;

        private const short MAX_QUALITY = 64;

        private List<Vector3> points = new List<Vector3>();

        private Vector3 startPoint;
        private Vector3 endPoint;

        // CONSTRUCTOR
        // -----------------------------------------------

        public Edge(Node source, Node destination, DisplayType displayType = DisplayType.SPRITE2D)
        {
            this.source = source;
            this.destination = destination;
            this.displayType = displayType;

            this.startPoint = source.GetPosition();
            this.endPoint = destination.GetPosition();

            graphics = new Graphics(this, PrimitiveType.LineStrip);

           // linesegments.Add(new LineSegment(startPoint, (endPoint-startPoint) / 2 + new Vector3(0,10f,0), endPoint));
            linesegments.Add(new LineSegment(startPoint, Vector3.Zero, endPoint));

            for(float step = 0; step <= 1; step += quality)
            {
                points.Add(GetQuadraticBezierPoint(linesegments[0],step));
            }

            VertexPositionColor[] vertexColorData = new VertexPositionColor[points.Count];

            //Console.WriteLine("SIZE OF POINTS :" +  points.Count);

            for(int i = 0; i < points.Count; i++)
            {
                vertexColorData[i] = new VertexPositionColor(points[i], Color.Red);
            }


            graphics.SetVertexPositionColor(vertexColorData);
            graphics.SetPrimitiveCount(points.Count - 1);
        }


        

        // METHODS & FUNCTIONS
        // -----------------------------------------------
        public override void Draw(GraphicsDevice graphicsDevice, BasicEffect effect)
        {

            if(displayType.Equals(DisplayType.SPRITE2D))
            {
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    graphicsDevice.SetVertexBuffer(graphics.VertexBuffer);
                    graphicsDevice.DrawPrimitives(graphics.GetPrimitiveType(), 0, graphics.GetPrimitiveCount());
                }
            }
            else
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

            float x = (1 - t) * (1 - t) * start.X + (2 - 2 * t) * t + control.X + t * t * end.X;
            float y = (1 - t) * (1 - t) * start.Y + (2 - 2 * t) * t + control.Y + t * t * end.Y;
            float z = (1 - t) * (1 - t) * start.Z + (2 - 2 * t) * t + control.Z + t * t * end.Z;

            return new Vector3(x, y, z);
        }

        // PROPERTIES
        // -----------------------------------------------

   
    }
}
