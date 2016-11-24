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


        // CONSTRUCTOR
        // -----------------------------------------------

        public WorldContainer()
        {
            this.room = new Room(Vector3.Zero, 20,20,20);
            this.graphs = room.GetGraphs();

            this.source = new Node(new Vector3(0,0,0), GraphComponent.DisplayType.MODEL3D, 0.05f);
            this.destination = new Node(new Vector3(1,0,1), GraphComponent.DisplayType.MODEL3D, 0.05f);
            this.test = new Node(new Vector3(2, 1, 7), GraphComponent.DisplayType.MODEL3D, 0.05f);
            Floor floor = room.GetFloor();

            RegisterEntity(floor);
            RegisterEntity(source);
            RegisterEntity(destination);
            RegisterEntity(test);
            RegisterEntity(new Edge(source, destination));
            RegisterEntity(new Edge(test, destination));
            RegisterEntity(new Edge(test, source));
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
