using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameWindows.ModelComponents
{
    class Graphics
    {

        protected VertexBuffer vertexBuffer;
        protected VertexPositionColor[] vertexPositionColor;

        protected PrimitiveType type;

        private int primitiveCount = 0;

        public VertexBuffer VertexBuffer {
            get { return vertexBuffer; }
            set { vertexBuffer = value; }
        }


        public Graphics(PrimitiveType type)
        {
            this.type = type;
        }

        public Graphics(PrimitiveType type, VertexPositionColor[] vertexPositionColor, int primitiveCount)
        {
            this.vertexPositionColor = vertexPositionColor;
            this.primitiveCount = primitiveCount;
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


        public void Draw(BasicEffect effect)
        {

        }

    }
}
