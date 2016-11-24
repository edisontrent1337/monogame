using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonogameWindows.Models;
namespace MonogameWindows.ModelComponents
{
    class Graphics
    {

        protected VertexBuffer vertexBuffer;
        protected VertexPositionColor[] vertexPositionColor;

        protected PrimitiveType primitiveType;

        private int primitiveCount = 0;

        private Entity entity;

        public VertexBuffer VertexBuffer {
            get { return vertexBuffer; }
            set { vertexBuffer = value; }
        }


        public Graphics(Entity entity, PrimitiveType type)
        {
            this.entity = entity;
            this.primitiveType = type;
            entity.EnableGraphics();
        }

        public Graphics(Entity entity, PrimitiveType type, VertexPositionColor[] vertexPositionColor, int primitiveCount)
        {
            this.entity = entity;
            this.vertexPositionColor = vertexPositionColor;
            this.primitiveCount = primitiveCount;
            entity.EnableGraphics();
        }

        public void SetVertexBuffer(VertexBuffer vertexBuffer)
        {
            this.vertexBuffer = vertexBuffer;
            vertexBuffer.SetData<VertexPositionColor>(vertexPositionColor);
        }

        public void SetVertexPositionColor(VertexPositionColor[] vertexPositionColor)
        {
            this.vertexPositionColor = vertexPositionColor;
        }


        public VertexPositionColor[] GetVertexPositionColor()
        {
            return vertexPositionColor;
        }


        public void SetPrimitiveCount(int primitiveCount)
        {
            this.primitiveCount = primitiveCount;
        }

        public int GetPrimitiveCount()
        {
            return primitiveCount;
        }

        public void SetPrimitiveType(PrimitiveType type)
        {
            this.primitiveType = type;
        }

        public PrimitiveType GetPrimitiveType()
        {
            return primitiveType;
        }



    }
}
