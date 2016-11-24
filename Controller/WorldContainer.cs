using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.Entities.Room;
using Microsoft.Xna.Framework;
using MonogameWindows.Entities.Graphs;
using MonogameWindows.Entities;

using MonogameWindows.Entities.GraphComponents.Egde;
using MonogameWindows.Entities.GraphComponents.Nodes;
using MonogameWindows.Entities.GraphComponents;


namespace MonogameWindows.Controller
{
    class WorldContainer
    {
        //TODO: implement tango stuff
        private Room room;
        private HashSet<Graph> graphs = new HashSet<Graph>();
        private Dictionary<int,Entity> entities = new Dictionary<int,Entity>();

        // TESTING

        private Node source, destination, test;

        private List<Node> nodes = new List<Node>();
        // CONSTRUCTOR
        // -----------------------------------------------

        public WorldContainer()
        {
            this.room = new Room(Vector3.Zero, 20,20,20);
            this.graphs = room.GetGraphs();

            /*this.source = new Node(new Vector3(0,0,0), GraphComponent.DisplayType.MODEL3D);
            this.destination = new Node(new Vector3(1,0,1), GraphComponent.DisplayType.MODEL3D);
            this.test = new Node(new Vector3(2, 1, 7), GraphComponent.DisplayType.MODEL3D);*/

            Random random = new Random();

            for(int i = 0; i < 100; i++)
            {
                int x = random.Next(0, (int)room.Width);
                int y = random.Next(0, (int)room.Height);
                int z = random.Next(0, (int)room.Depth);

                int r = random.Next(0, 255);
                int g = random.Next(0, 255);
                int b = random.Next(0, 255);
                nodes.Add(new Node(new Vector3(x, y, z), new Color(r,g,b,255), GraphComponent.DisplayType.MODEL3D));
            }

            Floor floor = room.GetFloor();

            RegisterEntity(floor);

            foreach(Node n in nodes)
            {
                RegisterEntity(n);
            }


            for(int i = 0; i < 500; i++)
            {
                Edge e = new Edge(nodes[random.Next(0, nodes.Count - 1)], nodes[random.Next(0, nodes.Count - 1)]);
                RegisterEntity(e);
            }

            /*RegisterEntity(new Edge(source, destination));
            RegisterEntity(new Edge(test, destination));
            RegisterEntity(new Edge(test, source));*/
            //RegisterEntity(new Edge(new Vector3(1,2,2), destination.GetPosition()));
            //RegisterEntity(new Edge(new Vector3(1,2,2), source.GetPosition()));

            foreach (Entity e in entities.Values) {
                Console.WriteLine(e.GetID());
            }


            Console.WriteLine("ENTITY COUNT :" + Entity.entityCount);
        }

        public WorldContainer(int width, int height, int depth)
        {
            this.room = new Room(Vector3.Zero, width, height, depth);
            this.graphs = room.GetGraphs();
        }

        // METHODS & FUNCTIONS
        // -----------------------------------------------

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
            entities.Add(e.GetID(), e);
        }


        // PROPERTIES
        // -----------------------------------------------

    }
}
