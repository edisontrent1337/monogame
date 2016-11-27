using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace RainBase.Entities.GraphComponents.Egde
{
    class LineSegment
    {

        private Vector3 start, end, control;
        // CONSTRUCTOR
        // -----------------------------------------------

        public LineSegment(Vector3 start, Vector3 control, Vector3 end)
        {
            this.start = start;
            this.end = end;
            this.control = control;
        }

        // METHODS & FUNCTIONS
        // -----------------------------------------------


        // PROPERTIES
        // -----------------------------------------------

        public Vector3 Start
        {
            get { return start; }
            set { start = value; }
        }

        public Vector3 Control
        {
            get { return control; }
            set { control = value; }
        }

        public Vector3 End
        {
            get { return end; }
            set { end = value; }
        }

    }
}
