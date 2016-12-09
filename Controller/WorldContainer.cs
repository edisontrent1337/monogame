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
using RainBase.EntityComponents;
using Microsoft.Xna.Framework.Graphics;
using RainBase.VertexType;

namespace RainBase.Controller
{
    public class WorldContainer
    {
        //TODO: implement tango stuff
        private Room room;
        //private HashSet<Graph> graphs = new HashSet<Graph>();
        //private Graph graph;
        private Dictionary<int,Entity> entities = new Dictionary<int,Entity>();

        private Dictionary<int, Graph> graphs = new Dictionary<int, Graph>();
        private Game rain;
        // CONSTRUCTOR
        // -----------------------------------------------

        public WorldContainer(Game rain)
        {
            //this.room = new Room(Vector3.Zero, 5f,3.5f,10f);
            this.room = new Room(Vector3.Zero, 10f, 3f, 10f);
            Graph g1 = new Graph(6, 13, room);
            Graph g2 = new Graph(5, 4, room);
            g1.PopulateRandomly();
            g2.PopulateRandomly();
            this.rain = rain;

            foreach(Graph g in room.GetGraphs())
            {
                graphs.Add(g.GetID(), g); 
            }

            Floor floor = room.GetFloor();

            RegisterEntity(floor);

            foreach (Graph graph in graphs.Values) {

                foreach (GraphComponent gc in graph.GetGraphComponents())
                {
                   RegisterEntity(gc);
                }

            }
   
        }

        /*public WorldContainer(int width, int height, int depth)
        {
            this.room = new Room(Vector3.Zero, width, height, depth);
        }*/

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

        /// <summary>
        /// Registers an entity in the main entity Dictionary that is used to render the whole
        /// scene.
        /// </summary>
        /// <param name="e">Entity to be added</param>
        public void RegisterEntity(Entity e)
        {
            entities.Add(e.GetEntityID(), e);
        }

        /**
         * TODO: Either provide two vertexbuffers, one for 2d and one for 3d,
         * or implement a function like the one below that flips a primitive from 2d to 3d and vice versa.
         * TODO: implement function Flip(int entityID, DisplayType displayType)
         * TODO: implement funtion EnableBezier(int edgeID)
         * TODO: implement Update() function for Graph, so that the entities hashmap of the worldcontainer
         * is consistent with the graphcomponents set in each graph
         * TODO: implement GetGraphComponentByID(int ID) method so that we can grab an edge or node by its id and
         * modify it.
         * 
         * FLIP works but it is shit.
        **/
  

        public Dictionary<int,Graph> GetGraphs()
        {
            return graphs;
        }

        /// <summary>
        /// Returns the graph specified by the given id.
        /// If the id is invalid, a new ArgumentOutOfRangeException is thrown.
        /// </summary>
        /// <param name="ID">Id of the graph</param>
        /// <returns>Graph g</returns>
        public Graph GetGraphById(int ID)
        {
            if (graphs.ContainsKey(ID))
                return graphs[ID];
            else
                throw new ArgumentOutOfRangeException("THE GRAPH SPECIFIED BY THE ID " + ID + " DOES NOT EXIST");
        }
        // PROPERTIES
        // -----------------------------------------------

    }
}
