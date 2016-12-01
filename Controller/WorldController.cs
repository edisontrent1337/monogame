using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RainBase.Entities.RoomComponents;
using RainBase.Main;
using Microsoft.Xna.Framework;

namespace RainBase.Controller
{
    public class WorldController
    {

        private WorldContainer worldContainer;

        private Game rain;
        // CONSTRUCTOR
        // -----------------------------------------------

        // WINDOWS
        public WorldController(Game rain, WorldContainer worldContainer)
        {
            this.rain = rain;
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
