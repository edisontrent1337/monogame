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


namespace MonogameWindows.Controller
{
    class WorldContainer
    {
        //TODO: implement tango stuff
        private Room room;
        private HashSet<Graph> graphs = new HashSet<Graph>();
        private Dictionary<int,Entity> entities = new Dictionary<int,Entity>();


        // TESTING

        private Edge edge;
        private Node source;
        private Node destination;


        // CONSTRUCTOR
        // -----------------------------------------------

        public WorldContainer()
        {
            this.room = new Room(Vector3.Zero, 20,20,20);
            this.graphs = room.GetGraphs();

            this.source = new Node(new Vector3(0,0,0));
            this.destination = new Node(new Vector3(1,0,1));
            Floor floor = room.GetFloor();

            RegisterEntity(floor);
            RegisterEntity(source);
            RegisterEntity(destination);
            RegisterEntity(new Edge(source, destination,1));
            RegisterEntity(new Edge(new Vector3(1,2,2), destination.GetPosition()));
            RegisterEntity(new Edge(new Vector3(1,2,2), source.GetPosition()));

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
