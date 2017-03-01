using RainWindows.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RainBase.Cameras;
using Microsoft.Xna.Framework.Input;
using RainBase.Entities.Graphs;
using RainBase.Entities.GraphComponents.Egde;
using RainBase.Entities.GraphComponents.Nodes;
using RainBase.Entities.GraphComponents;

namespace RainBase.Controller
{
    class WindowsInputHandler : ICameraController
    {
        private FirstPersonCamera camera;
        private WorldController worldController;
        private WorldContainer worldContainer;
        private KeyboardState oldKeyboardState = Keyboard.GetState();
        private KeyboardState currentKeyboardState;
        private MouseState oldMouseState = Mouse.GetState();
        private MouseState currentMouseState;
        private GraphComponent currentGraphComponent;


        Dictionary<Keys, bool> keyMap = new Dictionary<Keys, bool>();


        // CONSTRUCTOR
        // -----------------------------------------------
        public WindowsInputHandler(FirstPersonCamera camera, WorldController worldController, WorldContainer worldContainer)
        {
            this.camera = camera;
            this.worldController = worldController;
            this.worldContainer = worldContainer;
        }

        // METHODS & FUNCTIONS
        // -----------------------------------------------
        public void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();


            // KEYBOARD UPDATE LOGIC
            if (currentGraphComponent != null)
            {
                if (KeyWasTapped(Keys.F))
                {
                      worldController.Toggle3D(currentGraphComponent);
                }

                if (KeyWasTapped(Keys.B))
                {
                    if (currentGraphComponent.GetType().Equals(typeof(Edge)))
                        worldController.ToggleBezier((Edge)currentGraphComponent);
                }

                if(KeyWasTapped(Keys.OemPlus))
                {
                    if (currentGraphComponent.GetType().Equals(typeof(Edge)))
                        worldController.EnhanceSmoothness((Edge)currentGraphComponent);
                }

                if (KeyWasTapped(Keys.OemMinus))
                {
                    if (currentGraphComponent.GetType().Equals(typeof(Edge)))
                        worldController.DecreaseSmoothness((Edge)currentGraphComponent);
                }

 

            }

            oldKeyboardState = currentKeyboardState;

            //MOUSE UPDATE LOGIC
            int mouseX = currentMouseState.X;
            int mouseY = currentMouseState.Y;
            currentGraphComponent = CastPickRay(mouseX, mouseY);

        }

        /// <summary>
        /// Returns whether or not the specified Key k was pressed.
        /// </summary>
        /// <param name="k">The specified Key.</param>
        /// <returns></returns>
        private bool KeyWasTapped(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k) & !oldKeyboardState.IsKeyDown(k);
        }

        public void UpdateCamera(Vector3 position, Matrix rotation)
        {
            // camera.Update(position, rotation);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Casts a ray into the scene, starting at the specified screen coordinates.
        /// Returns the GraphComponent the user is currently looking or pointing at.
        /// This method can be used to implement interaction functions, allowing the user
        /// to manipulate single graph components of a node link diagram visualization.
        /// </summary>
        /// <param name="screenX"> cursor x position</param>
        /// <param name="screenY"> cursor y position</param>
        /// <returns></returns>
        public GraphComponent CastPickRay(int screenX, int screenY)
        {
            GraphComponent result = null;
            // getting the screen coordinates to be unprojected
            Vector3 nearSource = new Vector3((float)screenX, (float)screenY, 0);
            Vector3 farSource = new Vector3((float)screenX, (float)screenY, 1f);

            Matrix world = Matrix.CreateTranslation(0, 0, 0);

            // calcultion of the unprojected screen coordinates
            Vector3 nearPoint = camera.GetViewport().Unproject(nearSource, camera.Projection, camera.View, world);
            Vector3 farPoint = camera.GetViewport().Unproject(farSource, camera.Projection, camera.View, world);

            // direction of the pick ray
            Vector3 dir = farPoint - nearPoint;
            dir.Normalize();

            // creating a new ray starting at nearpoint in direction dir
            Ray r = new Ray(nearPoint, dir);

            float? dst = float.MaxValue;
            float? intersection = null;
            // iterate over all graphs in the scene to find the graph component closest to the specified screen coordinates
            foreach (Graph g in worldContainer.GetGraphs().Values)
            {
                foreach (GraphComponent gc in g.GetGraphComponents())
                {
                    foreach (BoundingBox b in gc.GetBoundingBoxes())
                    {
                        // find the intersetion distance by invoking an intersection test between the ray and the bounding box of the current graph component
                        intersection = r.Intersects(b);
                        // if the intersection is not null, a new intersection was found so the result is updated
                        if (!intersection.Equals(null))
                        {
                            if (intersection < dst)
                            {
                                dst = intersection;
                                result = gc;
                            }
                        }
                    }
                }


                if (result != null)
                {
                   // Console.WriteLine(result);
                }
            }
            return result;
        }
        // PROPERTIES
        // -----------------------------------------------
    }
}
