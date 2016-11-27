using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RainBase.Entities.Room;
using RainBase.Main;
using Rain;
using Microsoft.Xna.Framework;

namespace RainBase.Controller
{
    class WorldController
    {

        private WorldContainer worldContainer;

        private Game rain;
        // CONSTRUCTOR
        // -----------------------------------------------

        // WINDOWS
        public WorldController(Game rain, WorldContainer worldContainer)
        {
            //this.rainWindows = rainWindows;
            this.rain = rain;
            this.worldContainer = worldContainer;
        }

        // ANDROID
        /*public WorldController(RainAndroid rainMain, WorldContainer container)
        {
            // TODO: Complete member initialization
            this.rainAndroid = rainMain;
            this.worldContainer = container;
        }*/

   
        // METHODS & FUNCTIONS
        // -----------------------------------------------

        public void ProcessTangoData()
        {

        }

        public void Update()
        {

        }
        // PROPERTIES
        // -----------------------------------------------
    }
}
