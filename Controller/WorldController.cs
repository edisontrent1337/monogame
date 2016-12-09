using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RainBase.Entities.RoomComponents;
using RainBase.Main;
using Microsoft.Xna.Framework;
using RainBase.Cameras;
using RainBase.Entities.Graphs;
using RainBase.Entities.GraphComponents.Egde;
using RainBase.Entities.GraphComponents.Nodes;
using RainBase.Entities.GraphComponents;
using static RainBase.Entities.GraphComponents.GraphComponent;
using RainBase.EntityComponents;
using Microsoft.Xna.Framework.Graphics;
using RainBase.VertexType;
using static RainBase.Entities.GraphComponents.Egde.Edge;

namespace RainBase.Controller
{
    /// <summary>
    /// Class that is responsible for updating entities position and other attributes.
    /// Also updates position and orientation of the camera used for rendering.
    /// </summary>
    public class WorldController
    {

        private WorldContainer worldContainer;

        private Game rain;
        private FirstPersonCamera camera;
        // CONSTRUCTOR
        // -----------------------------------------------

        // WINDOWS
        public WorldController(Game rain, WorldContainer worldContainer)
        {
            this.rain = rain;
            this.worldContainer = worldContainer;
            //this.camera = new FirstPersonCamera(new Vector3(5, 1.80f, 5), Vector3.UnitZ, 3, graphicsDevice);

        }


        // METHODS & FUNCTIONS
        // -----------------------------------------------

        public void ProcessTangoData()
        {

        }


        public void Toggle3D(Graph g)
        {
            foreach(GraphComponent gc in g.GetGraphComponents())
            {
                Toggle3D(gc);
            }
        }

        /// <summary>
        /// Toogles 3D rendering state for a single GraphComponent.
        /// Disposes the old VertexBuffer and creates a new one with updated 2D or 3D data.
        /// </summary>
        /// <param name="component">Edge or Node to be modified.</param>
        public void Toggle3D(GraphComponent component)
        {
            Graphics g = component.GetGraphics(); 
            g.VertexBuffer.Dispose();

            if(component.GetDisplayType().Equals(DisplayType.MODEL3D))
                component.SetupGraphicsComponent(DisplayType.MODEL2D);

            else
                component.SetupGraphicsComponent(DisplayType.MODEL3D);

            g = component.GetGraphics();
            VertexBuffer vertexBuffer = new VertexBuffer(rain.GraphicsDevice, typeof(VertexPositionNormalColor), g.GetVertexPositionNormalColor().Length, BufferUsage.WriteOnly);
            g.SetUpVertexBufferAndData(vertexBuffer);

            if (g.IndexedRendering)
            {
                IndexBuffer ib = new IndexBuffer(rain.GraphicsDevice, typeof(ushort), g.GetIndices().Count, BufferUsage.WriteOnly);
                g.SetUpIndicesAndData(ib);
            }
        }


        public void Toggle3D(int graphID)
        {
            Toggle3D(worldContainer.GetGraphById(graphID));
        }

        public void ToggleTaperedEgde(Edge e)
        {

        }

        public void ToggleBezier(int graphID)
        {
            Graph g = worldContainer.GetGraphById(graphID);
            ToggleBezier(g);
        }

        public void ToggleBezier(Graph g)
        {
            foreach (Edge e in g.GetEdges())
                ToggleBezier(e);
        }

        public void ToggleBezier(Edge e)
        {
            DisplayType currentDisplayType = e.GetDisplayType();
            Graphics g = e.GetGraphics();
            Smoothness smoothness = e.GetLevelOfSmoothness();
            g.VertexBuffer.Dispose();


            if(!smoothness.Equals(Smoothness.NONE))
            {
                e.SetLevelOfSmoothness(Smoothness.NONE);
            }
            else
            {
                e.SetLevelOfSmoothness(Smoothness.LOW);
            }
            e.SetupGraphicsComponent();

            g = e.GetGraphics();
            VertexBuffer vertexBuffer = new VertexBuffer(rain.GraphicsDevice, typeof(VertexPositionNormalColor), g.GetVertexPositionNormalColor().Length, BufferUsage.WriteOnly);
            g.SetUpVertexBufferAndData(vertexBuffer);

        }
        /// <summary>
        /// Adds a node to the graph specified by the graphID.
        /// </summary>
        /// <param name="position">The position the new node is supposed to appear.</param>
        /// <param name="graphID">The graph the new node should be added to.</param>
        public void AddNode(Vector3 position, int graphID)
        {
            Graph g = worldContainer.GetGraphs()[graphID];
            Node newNode = g.AddNode(position);
            worldContainer.RegisterEntity(newNode);
        }
        /**
         * TODO: IMPLEMENT THIS
         **/
        public void AddEgde(Node a, Node b)
        {

        }

        /**
         * TODO: IMPLEMENT THIS
         **/
        public void RemoveEdge(int graphID, int edgeID)
        {
            if (graphID < 0 || graphID > worldContainer.GetGraphs().Count - 1)
                throw new ArgumentOutOfRangeException("THE GRAPH WITH THE FOLLOWING ID WAS NOT FOUND: " + graphID);

            Graph g = worldContainer.GetGraphById(graphID);

        }

        public void Update()
        {

        }
        // PROPERTIES
        // -----------------------------------------------
    }
}
