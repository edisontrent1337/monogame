using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.ModelComponents;

namespace MonogameWindows.Models
{
    class Entity
    {
        protected Graphics graphics;
        private bool hasGraphics = false;


        public Entity()
        {

        }

        public void EnableGraphics()
        {
            hasGraphics = true;
        }

        public bool HasGraphics()
        {
            return hasGraphics;
        }

    }
}
