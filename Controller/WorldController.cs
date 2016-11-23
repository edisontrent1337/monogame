using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.Models.Room;
using MonogameWindows.Main;

namespace MonogameWindows.Controller
{
    class WorldController
    {
        private Game1 game;
        private WorldContainer worldContainer;

        private Room room;

        // CONSTRUCTOR
        // -----------------------------------------------

        public WorldController(Game1 game, WorldContainer worldContainer)
        {
            this.game = game;
            this.worldContainer = worldContainer;
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
