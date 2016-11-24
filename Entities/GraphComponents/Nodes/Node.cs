using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameWindows.Entities.GraphComponents.Egde;

namespace MonogameWindows.Entities.GraphComponents.Nodes
{
    class Node : GraphComponent
    {

        private Vector3 position;

        private Dictionary<int, Edge> edges = new Dictionary<int, Edge>();

        // CONSTRUCTOR
        // -----------------------------------------------

        public Node(Vector3 position)
        {
            this.position = position;
        }


        // METHODS & FUNCTIONS
        // -----------------------------------------------


        public void AddEdge(Edge e)
        {
            //edges.Add(e.Get)
        }


        public void RemoveEdge(Edge e)
        {

        }


        public override void Draw(GraphicsDevice graphicsDevice, BasicEffect effect)
        {
            if(displayType.Equals(DisplayType.SPRITE2D))
            {

            }

            else if(displayType.Equals(DisplayType.MODEL3D))
            {

            }

            base.Draw(graphicsDevice, effect);
        }


        public override void Update()
        {
            base.Update();
        }

        // PROPERTIES
        // -----------------------------------------------
    }
}
