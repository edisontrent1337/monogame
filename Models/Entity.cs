using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.ModelComponents;
using Microsoft.Xna.Framework.Graphics;
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


        public virtual void Update()
        {

        }

        public virtual void Draw(GraphicsDevice graphicsDevice, BasicEffect effect)
        {

        }

        public Graphics GetGraphics()
        {
            return graphics;
        }

    }
}
