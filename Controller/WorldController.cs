using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.Entities.Room;
using MonogameWindows.Main;
using Rain;
using Microsoft.Xna.Framework;

namespace MonogameWindows.Controller
{
    class WorldController
    {
        private RainWindows rainWindows;
        private RainAndroid rainAndroid;

        private WorldContainer worldContainer;
        private Room room;

        // CONSTRUCTOR
        // -----------------------------------------------

        // WINDOWS
        public WorldController(RainWindows rainWindows, WorldContainer worldContainer)
        {
            this.rainWindows = rainWindows;
            this.worldContainer = worldContainer;
        }

        // ANDROID
        public WorldController(RainAndroid rainMain, WorldContainer container)
        {
            // TODO: Complete member initialization
            this.rainAndroid = rainMain;
            this.worldContainer = container;
        }

   
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
