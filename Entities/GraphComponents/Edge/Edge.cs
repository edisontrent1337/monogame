using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.Entities.GraphComponents.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameWindows.EntityComponents;
namespace MonogameWindows.Entities.GraphComponents.Egde
{
    class Edge : GraphComponent
    {



        private Node source, destination;
        private List<LineSegment> linesegments;

        private float thickness = 1f;
        private short quality = 2;
        private short segmentQuality = 2;

        private const short MAX_QUALITY = 64;

        private List<Vector3> points;

        private Vector3 startPoint;
        private Vector3 endPoint;


        // CONSTRUCTOR
        // -----------------------------------------------

        public Edge(Node source, Node destination, short quality, DisplayType displayType = DisplayType.SPRITE2D)
        {
            this.source = source;
            this.destination = destination;

            this.displayType = displayType;

            graphics = new Graphics(this, PrimitiveType.LineList);

            this.startPoint = source.GetPosition();
            this.endPoint = destination.GetPosition();

            VertexPositionColor[] test = new VertexPositionColor[2];

            test[0] = new VertexPositionColor(startPoint, Color.Red);
            test[1] = new VertexPositionColor(endPoint, Color.Red);


            graphics.SetVertexPositionColor(test);
            graphics.SetPrimitiveCount(1);
        }


        public Edge(Vector3 start, Vector3 end) : this(new Node(start), new Node(end), 1)
        {

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

        // PROPERTIES
        // -----------------------------------------------

   
    }
}
