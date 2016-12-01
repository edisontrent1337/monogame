using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RainBase.Entities;
using RainBase.VertexType;

namespace RainBase.EntityComponents
{
   public class Graphics
    {

        protected VertexBuffer vertexBuffer;
        protected VertexPositionColor[] vertexPositionColor;
        protected VertexPositionNormalColor[] vertexPositionNormalColor;
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
            vertexBuffer.SetData<VertexPositionNormalColor>(vertexPositionNormalColor);
        }

        public void SetVertexPositionColor(VertexPositionColor[] vertexPositionColor)
        {
            this.vertexPositionColor = vertexPositionColor;
        }

        public void SetVertexPositionNormalColor(VertexPositionNormalColor[] vertexPositionNormalColor)
        {
            this.vertexPositionNormalColor = vertexPositionNormalColor;
        }

        public VertexPositionColor[] GetVertexPositionColor()
        {
            return vertexPositionColor;
        }


        public VertexPositionNormalColor[] GetVertexPositionNormalColor() {
            return vertexPositionNormalColor;
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
