using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RainBase.Cameras;


using RainBase.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RainBase.EntityComponents;

namespace RainBase.Entities.GraphComponents
{
    public class GraphComponent : Entity
    {

        public static int componentCount = 0;
        protected Color color;
        private Texture2D texture;
        protected List<BoundingBox> boundingBoxes = new List<BoundingBox>();


        

        protected readonly int ID;

        private Random random = new Random();


        public enum RenderState
        {
            MODEL2D,
            MODEL3D
        }


        protected RenderState renderState;

        // CONSTRUCTOR
        // -----------------------------------------------
        public GraphComponent()
        {
            ID = componentCount;
            componentCount++;
        }

        public GraphComponent(Texture2D texture)
        {

        }

        // METHDODS & FUNCTIONS
        // -----------------------------------------------

        public float GetDistanceToCamera()
        {
            return distanceToCamera;
        }

        public void SetDistanceToCamera(Camera camera)
        {
            //TODO: implement this.
        }

        public Color GetRandomColor()
        {
            int r = random.Next(0, 255);
            int g = random.Next(0, 255);
            int b = random.Next(0, 255);

            return new Color(r, g, b, 255);
        }


        // PROPERTIES
        // -----------------------------------------------
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        

        public void SetDisplayType(RenderState displayType)
        {
            this.renderState = displayType;
        }

        public virtual int GetID()
        {
            return -1;
        }

        public override void Draw(GraphicsDevice graphicsDevice, BasicEffect effect, Effect custom)
        {
            base.Draw(graphicsDevice, effect, custom);
        }


        public virtual Graphics SetupGraphicsComponent()
        {
            return graphics;
        }

        public virtual Graphics SetupGraphicsComponent(RenderState displayType)
        {
            return graphics;
        }

        public virtual List<BoundingBox> GetBoundingBoxes()
        {
            return boundingBoxes;
        }

        public Color GetColor()
        {
            return color;
        }

        public RenderState GetRenderState()
        {
            return renderState;
        }
    }
}
