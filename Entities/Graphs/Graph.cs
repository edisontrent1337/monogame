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
    /**
     * Class that holds graph data. Manages a Set of GraphComponents,
     * a list of nodes and a list of edges.
     **/
    public class Graph
    {
        private static int idCounter = 0;
        private short numberOfNodes = 0;
        private short numberOfEdges = 0;
        private short totalComponents = 0;
        private bool connected = false;
        private Room room;

        private const int MAX_TRIES = 1000;
        private readonly int GRAPH_ID;

        private HashSet<GraphComponent> graphComponents = new HashSet<GraphComponent>();
        private List<Node> nodes = new List<Node>();
        private List<Edge> edges = new List<Edge>();
        private short[,] adjacencyMatrix;

        private Node root;

        //TODO: implement adjacency matrix

        // CONSTRUCTOR
        // -----------------------------------------------
        public Graph(short numberOfNodes, short numberOfEdges, Room room)
        {
            if (numberOfEdges > (numberOfNodes*(numberOfNodes-1)/ 2))
               throw new ArgumentOutOfRangeException("NUMBER OF EDGES MUST BE EQUAL OR LESS THAN n*(n-1) / 2 .");
            this.numberOfNodes = numberOfNodes;
            this.numberOfEdges = numberOfEdges;
        
            this.room = room;
            adjacencyMatrix = new short[numberOfNodes, numberOfNodes];

            GRAPH_ID = idCounter;
            idCounter++;
            room.AddGraph(this);
        }



        // METHODS & FUNCTIONS
        // -----------------------------------------------

        /**
         * Populates a graph data structure randomly by executing the following steps:
         * 1) Place node at random positions
         * 2) As long as there are not enough edges, choose two nodes and try to establish a connection.
         **/
        public void PopulateRandomly()
        {
            Random random = new Random();

            for (int i = 0; i < numberOfNodes; i++)
            {
                float x = (float)random.NextDouble() * room.Width;
                float y = (float)random.NextDouble() * room.Height;
                float z = (float)random.NextDouble() * room.Depth;
                nodes.Add(new Node(new Vector3(x, y, z), GRAPH_ID, i, GraphComponent.RenderState.MODEL3D, 0.005f));
            }

            // Try to add edges as long as there are not enough of them.
            for (int i = 0; i < MAX_TRIES; i++)
            {
                if (edges.Count < numberOfEdges)
                {
                    Node nodeA = nodes[random.Next(0, nodes.Count - 1)];
                    Node nodeB = nodes[random.Next(0, nodes.Count - 1)];

                    // If there is no connection yet between the nodes A & B, and if A is not the same node as B,
                    // create a new edge, setup its graphics component and add it to the edges list.
                    if (adjacencyMatrix[nodeA.GetID(), nodeB.GetID()] == 0 && adjacencyMatrix[nodeB.GetID(), nodeA.GetID()] == 0 & !(nodeA.Equals(nodeB)))
                    {
                        Edge e = new Edge(nodeA, nodeB, GRAPH_ID, edges.Count, GraphComponent.RenderState.MODEL3D, GetRandomColor(random));
                        edges.Add(e);
                        adjacencyMatrix[nodeA.GetID(), nodeB.GetID()] = 1;
                    }
                }
            }
            totalComponents = (short)(nodes.Count + edges.Count);

            graphComponents.UnionWith(nodes);
            graphComponents.UnionWith(edges);

            Console.WriteLine("\n POPULATED GRAPH WITH FOLLOWING ID RANDOMLY: " + GRAPH_ID);
            Console.WriteLine("\t NUMBER OF NODES: " + nodes.Count);
            foreach (Node n in nodes)
                Console.WriteLine("\t\t" + n);
            Console.WriteLine("\t NUMBER OF EDGES: " + edges.Count);
            foreach (Edge e in edges)
                Console.WriteLine("\t\t" + e);
            Console.WriteLine("\t TOTAL NUMBER OF COMPONENTS: " + totalComponents);
            PrintAdjacencyMatrix();
        }

        /// <summary>
        /// Prints the adjacency matrix of the graph.
        /// </summary>
        private void PrintAdjacencyMatrix()
        {
            Console.WriteLine("\t ADJACENCY MATRIX FOR GRAPH :" + GRAPH_ID);

            for (int x = -1; x < numberOfNodes; x++)
            {

                if (x == -1)
                {
                    for (int i = 0; i < numberOfNodes; i++)
                    {
                        if (i == 0)
                            Console.Write("\t\t" + "    i" + i);
                        else
                            Console.Write("\t" + "    i" + i);

                    }
                    Console.WriteLine("");
                }
                else
                {
                    for (int y = -1; y < numberOfNodes; y++)
                    {
                        if (y == -1)
                            Console.Write("\t i" + x + "\t");
                        else
                            Console.Write("\t" + adjacencyMatrix[x, y] + "\t");
                    }
                    Console.WriteLine("");
                }
            }

        }
    

        public short GetNumberOfEdges()
        {
            return numberOfEdges;
        }

        public short GetNumberOfNodes()
        {
            return numberOfNodes;
        }

        public HashSet<GraphComponent> GetGraphComponents()
        {
            return graphComponents;
        }

        /// <summary>
        /// Adds a node to the internal data structure of the graph.
        /// Returns the node so it can be registered in the global entity hashset by the worldcontainer.
        /// </summary>
        /// <param name="position"> position the new node will occur</param>
        /// <returns>the newly created node</returns>
        public Node AddNode(Vector3 position)
        {
            Node n = new Node(position, GRAPH_ID, nodes.Count + 1);
            nodes.Add(n);
            return n;
        }

        /**
         * TODO: IMPLEMENT THIS
         **/
        private void UpdateAdjacencyMatrix()
        {

        }


        // PROPERTIES
        // -----------------------------------------------

        public Color GetRandomColor(Random random)
        {
            int r = random.Next(0, 255);
            int g = random.Next(0, 255);
            int b = random.Next(0, 255);

            return new Color(r, g, b);
        }

        public int GetID()
        {
            return GRAPH_ID;
        }

        public List<Edge> GetEdges()
        {
            return edges;
        }

        public Edge GetEdgeByID(int ID)
        {
            return edges[0];
        }

        public List<Node> GetNodes()
        {
            return nodes;
        }

    }
}
