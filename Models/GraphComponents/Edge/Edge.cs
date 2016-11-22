using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.Models.GraphComponents.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameWindows.Models.GraphComponents.Egde
{
    class Edge : GraphComponent
    {

        public enum EdgeType
        {
            LINE, TUBE
        }

        private Node source, destination;
        private List<LineSegment> linesegments;

        private float thickness = 1f;
        private short quality = 2;
        private short segmentQuality = 2;

        private const short MAX_QUALITY = 64;

        private List<Vector3> points;
        private EdgeType type;


        // CONSTRUCTOR
        // -----------------------------------------------

        public Edge(Node source, Node destination, short quality)
        {
            this.source = source;
            this.destination = destination;
        }


        // METHODS & FUNCTIONS
        // -----------------------------------------------
        public override void Draw(GraphicsDevice graphicsDevice)
        {

            if(type.Equals(EdgeType.LINE))
            {

            }
            else
            {

            }
            base.Draw(graphicsDevice);
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
