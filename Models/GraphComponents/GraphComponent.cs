using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonogameWindows.Cameras;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MonogameWindows.Models.GraphComponents
{
    class GraphComponent 
    {
        private Color color;
        private short id;
        private Texture2D texture;
        private float distanceToCamera;

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

        public virtual void Update()
        {

        }

        public virtual void Draw(GraphicsDevice device)
        {

        }


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
        

        public short ID
        {
            get { return id; }
        }
    }
}
