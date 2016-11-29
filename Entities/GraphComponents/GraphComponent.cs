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
    class GraphComponent : Entity
    {
        private Color color;
        private Texture2D texture;
        private float distanceToCamera;


        public enum DisplayType
        {
            MODEL2D,
            MODEL3D
        }


        protected DisplayType displayType;

        // CONSTRUCTOR
        // -----------------------------------------------
        public GraphComponent()
        {
           
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
        

        public void SetDisplayType(DisplayType displayType)
        {
            this.displayType = displayType;
        }

    }
}
