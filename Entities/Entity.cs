using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.EntityComponents;
using Microsoft.Xna.Framework.Graphics;
namespace MonogameWindows.Entities
{
    class Entity
    {
        protected Graphics graphics;
        private bool hasGraphics = false;

        public static short entityCount = 0;

        private short ID;

 


        public Entity()
        {

            ID = (short) (entityCount + 1);

            /*byte[] guidAsBytes = ID.ToByteArray();
            int test = new int(guidAsBytes);*/


            entityCount++;
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

        public int GetID()
        {
            return ID;
        }

    }
}
