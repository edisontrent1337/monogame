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

namespace RainBase.Controller
{
    class WindowsInputHandler : ICameraController
    {
        private FirstPersonCamera camera;
        private WorldController worldController;
        private WorldContainer worldContainer;
        private KeyboardState oldState = Keyboard.GetState();


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
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Graph g1 = worldContainer.GetGraphById(0);
            Edge g1e1 = g1.GetEdges()[0];
            if (currentKeyboardState.IsKeyDown(Keys.F) &!oldState.IsKeyDown(Keys.F))
            {


                //

               // worldController.Toggle3D(g1e1);
               worldController.Toggle3D(0);
               worldController.Toggle3D(1);

            }

            if (currentKeyboardState.IsKeyDown(Keys.B) &!oldState.IsKeyDown(Keys.B))
            {
                //

                //worldController.ToggleBezier(g1e1);

                worldController.ToggleBezier(0);
                worldController.ToggleBezier(1);
            }

            oldState = currentKeyboardState;
        }



        public void UpdateCamera(Vector3 position, Matrix rotation)
        {
           // camera.Update(position, rotation);
            throw new NotImplementedException();
        }
        // PROPERTIES
        // -----------------------------------------------
    }
}
