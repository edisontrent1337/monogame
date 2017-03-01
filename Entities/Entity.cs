using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RainBase.EntityComponents;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace RainBase.Entities
{
    public class Entity
    {
        protected Graphics graphics;
        private bool hasGraphics = false;
        protected float distanceToCamera;
        public static short entityCount = 0;

        private readonly short ENTITY_ID;


        public List<Effect> effects = new List<Effect>();

        public Entity()
        {

            ENTITY_ID = (short) (entityCount + 1);

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

        public virtual void Draw(GraphicsDevice graphicsDevice, BasicEffect effect, Effect custom)
        {

        }


        public virtual void Draw(GraphicsDevice graphicsDevice) {

        }


        public Graphics GetGraphics()
        {
            return graphics;
        }

        public int GetEntityID()
        {
            return ENTITY_ID;
        }

        public virtual void UpdateDistanceToCamera(Vector3 cameraPosition)
        {

        }
    }
}
