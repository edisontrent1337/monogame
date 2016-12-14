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


        // 2D / 3D TOGGLING
        // -----------------------------------------------
        /// <summary>
        /// Toggles 3D rendering state for a whole graph g.
        /// </summary>
        /// <param name="g">Graph to be modified.</param>
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
            Graphics graphics = component.GetGraphics(); 
            graphics.VertexBuffer.Dispose();

            // If the current display type is 3D, change it to 2D
            if(component.GetRenderState().Equals(RenderState.MODEL3D))
               graphics = component.SetupGraphicsComponent(RenderState.MODEL2D);

            // If the current display type is 2D, change it to 3D
            else
                graphics = component.SetupGraphicsComponent(RenderState.MODEL3D);

            // get the graph components graphic component and modify its vertex buffer
            VertexBuffer vertexBuffer = new VertexBuffer(rain.GraphicsDevice, typeof(VertexPositionNormalColor), graphics.GetVertexPositionNormalColor().Length, BufferUsage.WriteOnly);
            graphics.SetUpVertexBufferAndData(vertexBuffer);

            // if the underlying primitives use indexed rendering, initialize an indexbuffer and set it up accordingly.
            if (graphics.IndexedRendering)
            {
                IndexBuffer ib = new IndexBuffer(rain.GraphicsDevice, typeof(ushort), graphics.GetIndices().Count, BufferUsage.WriteOnly);
                graphics.SetUpIndicesAndData(ib);
            }
        }

        /// <summary>
        /// Toggles 3D rendering state for a whole graph.
        /// </summary>
        /// <param name="graphID">ID that specifies the graph to be modified.</param>
        public void Toggle3D(int graphID)
        {
            Toggle3D(worldContainer.GetGraphById(graphID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void ToggleTaperedEgde(Edge e)
        {

        }


        // BEZIER CURVES
        // -----------------------------------------------

        /// <summary>
        /// Toogles between a curved and a straight representation of edges for a whole graph.
        /// </summary>
        /// <param name="graphID">ID that specifies the graph to be modified.</param>
        public void ToggleBezier(int graphID)
        {
            Graph g = worldContainer.GetGraphById(graphID);
            ToggleBezier(g);
        }

        /// <summary>
        /// Toogles between a curved and a straight representation of edges for a whole graph.
        /// </summary>
        /// <param name="g">The graph to be modified.</param>
        public void ToggleBezier(Graph g)
        {
            foreach (Edge e in g.GetEdges())
                ToggleBezier(e);
        }

        /// <summary>
        /// Toogles between a curved and a straight representation of an edge.
        /// </summary>
        /// <param name="e">The edge to be modified.</param>
        public void ToggleBezier(Edge e)
        {
            Graphics graphics = e.GetGraphics();
            int smoothness = e.GetLevelOfSmoothness();
            graphics.VertexBuffer.Dispose();

            if(smoothness != (int) Smoothness.NONE)
            {
                e.SetLevelOfSmoothness((int)Smoothness.NONE);
            }
            else
            {
                e.SetLevelOfSmoothness((int)Smoothness.LOW);
            }

            graphics = e.SetupGraphicsComponent();
            VertexBuffer vertexBuffer = new VertexBuffer(rain.GraphicsDevice, typeof(VertexPositionNormalColor), graphics.GetVertexPositionNormalColor().Length, BufferUsage.WriteOnly);
            graphics.SetUpVertexBufferAndData(vertexBuffer);

        }


        private void ChangeSmoothness(Edge e, int direction)
        {
            // early exit this function, if the change in smoothness is 0
            if(direction == 0)
            {
                return;
            }

            Graphics graphics = e.GetGraphics();
            int smoothness = e.GetLevelOfSmoothness();

            // We do not allow negative values of smoothness or values above the maximum smoothness
            if (smoothness + direction <= 0 || smoothness + direction > (int)Smoothness.MAX)
                return;

            graphics.VertexBuffer.Dispose();

            e.SetLevelOfSmoothness(smoothness + direction);
            graphics = e.SetupGraphicsComponent();

            VertexBuffer vertexBuffer = new VertexBuffer(rain.GraphicsDevice, typeof(VertexPositionNormalColor), graphics.GetVertexPositionNormalColor().Length, BufferUsage.WriteOnly);
            graphics.SetUpVertexBufferAndData(vertexBuffer);
        }


        public void EnhanceSmoothness(Edge e)
        {
            ChangeSmoothness(e, 4);
        }

        public void DecreaseSmoothness(Edge e)
        {
            ChangeSmoothness(e, -4);
        }
        /// <summary>
        /// Adds a node to the graph specified by the graphID.
        /// </summary>
        /// <param name="position">The position the new node is supposed to appear.</param>
        /// <param name="graphID">The graph the new node should be added to.</param>
        public void AddNode(Vector3 position, int graphID)
        {
            Graph g = worldContainer.GetGraphById(graphID);
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
