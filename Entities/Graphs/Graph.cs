using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RainBase.Entities.GraphComponents;
using RainBase.Entities.GraphComponents.Nodes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RainBase.Entities.Graphs
{
    public class Graph
    {
        private short numberOfNodes = 0;
        private short numberOfEdges = 0;
        private bool connected = false;

        private HashSet<GraphComponent> graphComponents = new HashSet<GraphComponent>();
        private bool[,] adjacencyMatrix;

        private Node root;


        //TODO: implement adjacency matrix

        // CONSTRUCTOR
        // -----------------------------------------------
        public Graph(short numberOfNodes, short numberOfEdges)
        {
            this.numberOfNodes = numberOfNodes;
            this.numberOfEdges = numberOfEdges;
            adjacencyMatrix = new bool[numberOfNodes, numberOfNodes];
        }

        public Graph()
        {

        }


        // METHODS & FUNCTIONS
        // -----------------------------------------------


        public short GetNumberOfEdges()
        {
            return numberOfEdges;
        }

        public short GetNumberOfNodes()
        {
            return numberOfNodes;
        }


        public void Draw(GraphicsDevice graphicsDevice, BasicEffect basicEffect)
        {
            foreach (GraphComponent gc in graphComponents)
            {
                gc.Draw(graphicsDevice,basicEffect);
            }
        }



        // PROPERTIES
        // -----------------------------------------------


    }
}
