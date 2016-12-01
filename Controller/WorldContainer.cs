using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RainBase.Entities.RoomComponents;
using Microsoft.Xna.Framework;
using RainBase.Entities.Graphs;
using RainBase.Entities;

using RainBase.Entities.GraphComponents.Egde;
using RainBase.Entities.GraphComponents.Nodes;
using RainBase.Entities.GraphComponents;


namespace RainBase.Controller
{
    public class WorldContainer
    {
        //TODO: implement tango stuff
        private Room room;
        private HashSet<Graph> graphs = new HashSet<Graph>();
        private Dictionary<int,Entity> entities = new Dictionary<int,Entity>();

        // TESTING

        private Node source, destination, test;
        Random random = new Random();

        private List<Node> nodes = new List<Node>();
        // CONSTRUCTOR
        // -----------------------------------------------

        public WorldContainer()
        {
            //this.room = new Room(Vector3.Zero, 5f,3.5f,10f);
            this.room = new Room(Vector3.Zero, 10f,3f,10f);
            Graph g = new Graph(30, 30, room);

            g.PopulateRandomly();
            this.graphs = room.GetGraphs();

            Floor floor = room.GetFloor();

            RegisterEntity(floor);


            foreach(Graph graph in graphs) {

                foreach (GraphComponent gc in graph.GetGraphComponents())
                {
                    RegisterEntity(gc);
                }

            }
   

           /* for(int i = 0; i < 37; i++)
            {
                Edge e = new Edge(nodes[random.Next(0, nodes.Count - 1)], nodes[random.Next(0, nodes.Count - 1)], 0.0075f, GraphComponent.DisplayType.MODEL3D, GetRandomColor());
                RegisterEntity(e);
            }*/

            /*
            Edge a = new Edge(nodes[0], nodes[1],0.01f, GraphComponent.DisplayType.MODEL3D);
            Edge b = new Edge(nodes[0], nodes[1], 0.01f, GraphComponent.DisplayType.MODEL2D);
            Edge c = new Edge(nodes[2], nodes[1], 0.01f, GraphComponent.DisplayType.MODEL3D);
            Edge d = new Edge(nodes[1], origin, 0.01f, GraphComponent.DisplayType.MODEL3D);
            RegisterEntity(a);
            RegisterEntity(b);
            RegisterEntity(c);
            RegisterEntity(d);
            /*RegisterEntity(new Edge(source, destination));
            RegisterEntity(new Edge(test, destination));
            RegisterEntity(new Edge(test, source));*/
            //RegisterEntity(new Edge(new Vector3(1,2,2), destination.GetPosition()));
            //RegisterEntity(new Edge(new Vector3(1,2,2), source.GetPosition()));


            //Console.WriteLine("ENTITY COUNT :" + Entity.entityCount);
        }



        public WorldContainer(int width, int height, int depth)
        {
            this.room = new Room(Vector3.Zero, width, height, depth);
            this.graphs = room.GetGraphs();
        }

        // METHODS & FUNCTIONS
        // -----------------------------------------------

        private Color GetRandomColor()
        {
            int r = random.Next(0, 255);
            int g = random.Next(0, 255);
            int b = random.Next(0, 255);

            return new Color(r, g, b, 255);
        }

        public Room GetRoom()
        {
            return room;
        }

        public Dictionary<int,Entity> GetEntities()
        {
            return entities;
        }

        public List<Entity> GetEntityList()
        {
            return new List<Entity>(entities.Values);
        }

        public void RegisterEntity(Entity e)
        {
            entities.Add(e.GetEntityID(), e);
        }


        // PROPERTIES
        // -----------------------------------------------

    }
}
