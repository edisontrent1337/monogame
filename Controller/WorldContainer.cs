using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.Models.Room;
using Microsoft.Xna.Framework;
using MonogameWindows.Models.Graphs;
using MonogameWindows.Models.GraphComponents;

using MonogameWindows.Models;
namespace MonogameWindows.Controller
{
    class WorldContainer
    {
        //TODO: implement tango stuff
        private Room room;
        private HashSet<Graph> graphs = new HashSet<Graph>();
        private HashSet<Entity> entities = new HashSet<Entity>();


        // CONSTRUCTOR
        // -----------------------------------------------

        public WorldContainer()
        {
            this.room = new Room(Vector3.Zero, 20,20,20);
            this.graphs = room.GetGraphs();
            entities.Add(room.GetFloor());
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

        public HashSet<Entity> GetEntities()
        {
            return entities;
        }

        // PROPERTIES
        // -----------------------------------------------

    }
}
