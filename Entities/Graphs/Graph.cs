using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RainBase.Entities.GraphComponents;
using RainBase.Entities.GraphComponents.Nodes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainBase.Entities.RoomComponents;
using RainBase.Entities.GraphComponents.Egde;

namespace RainBase.Entities.Graphs
{
    public class Graph
    {
        private static int idCounter = 0;

        private short numberOfNodes = 0;
        private short numberOfEdges = 0;
        private bool connected = false;
        private Room room;

        private readonly int GRAPH_ID;

        private HashSet<GraphComponent> graphComponents = new HashSet<GraphComponent>();
        private List<Node> nodes = new List<Node>();
        private List<Edge> edges = new List<Edge>();
        private bool[,] adjacencyMatrix;

        private Node root;

        //TODO: implement adjacency matrix

        // CONSTRUCTOR
        // -----------------------------------------------
        public Graph(short numberOfNodes, short numberOfEdges, Room room)
        {
            this.numberOfNodes = numberOfNodes;
            this.numberOfEdges = numberOfEdges;
            this.room = room;
            adjacencyMatrix = new bool[numberOfNodes, numberOfNodes];

            GRAPH_ID = idCounter;
            idCounter++;
            room.AddGraph(this);
        }



        // METHODS & FUNCTIONS
        // -----------------------------------------------

        public void PopulateRandomly()
        {
            Random random = new Random();

            for (int i = 0; i < numberOfNodes; i++)
            {
                float x = (float)random.NextDouble() * room.Width;
                float y = (float)random.NextDouble() * room.Height;
                float z = (float)random.NextDouble() * room.Depth;

                nodes.Add(new Node(new Vector3(x, y, z), GRAPH_ID, GraphComponent.DisplayType.MODEL3D, 0.03f));
            }


            while(edges.Count < numberOfEdges)
            {
                Node nodeA = nodes[random.Next(0, nodes.Count - 1)];
                Node nodeB = nodes[random.Next(0, nodes.Count - 1)];


                if (!adjacencyMatrix[nodeA.GetID(),nodeB.GetID()])
                {
                    Edge e = new Edge(nodeA, nodeB, 0.002f, GraphComponent.DisplayType.MODEL3D); { }
                    edges.Add(e);
                    adjacencyMatrix[nodeA.GetID(), nodeB.GetID()] = true;
                }
            }

            graphComponents.UnionWith(nodes);
            graphComponents.UnionWith(edges);

        }

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
            foreach (GraphComponent gc in nodes)
            {
                gc.Draw(graphicsDevice, basicEffect);
            }
        }

        public HashSet<GraphComponent> GetGraphComponents()
        {
            return graphComponents;
        }

        // PROPERTIES
        // -----------------------------------------------


    }
}
