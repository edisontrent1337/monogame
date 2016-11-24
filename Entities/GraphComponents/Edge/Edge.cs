using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.Entities.GraphComponents.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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


        // CONSTRUCTOR
        // -----------------------------------------------

        public Edge(Node source, Node destination, short quality)
        {
            this.source = source;
            this.destination = destination;
        }


        // METHODS & FUNCTIONS
        // -----------------------------------------------
        public override void Draw(GraphicsDevice graphicsDevice, BasicEffect effect)
        {

            if(displayType.Equals(DisplayType.SPRITE2D))
            {

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

       public EdgeType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
