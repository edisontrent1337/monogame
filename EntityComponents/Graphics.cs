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
        protected IndexBuffer indexBuffer;

        protected VertexPositionNormalColor[] vertexPositionNormalColor;
        protected List<ushort> indices;
        protected PrimitiveType primitiveType;

        private bool indexedRendering = false;

        private int primitiveCount = 0;
        private Entity entity;



        public Graphics(Entity entity, PrimitiveType type)
        {
            this.entity = entity;
            this.primitiveType = type;
            entity.EnableGraphics();
        }

        public Graphics(Entity entity, PrimitiveType type, VertexPositionNormalColor[] vertexPositionNormalColor, List<ushort> indices, int primitiveCount)
        {
            this.entity = entity;
            this.vertexPositionNormalColor = vertexPositionNormalColor;
            this.indices = indices;
            this.primitiveCount = primitiveCount;
            entity.EnableGraphics();
        }

        /**
         * Sets the vertex buffer for this graphics component.
         * Immediatly fills the vertex buffer with vertex position normal color data.
         * This method should only be called if vertexPositionNormalColor was initialized.
         **/
        public void SetUpVertexBufferAndData(VertexBuffer vertexBuffer)
        {
            this.vertexBuffer = vertexBuffer;
            vertexBuffer.SetData<VertexPositionNormalColor>(vertexPositionNormalColor);
        }


        public void SetVertexPositionNormalColor(VertexPositionNormalColor[] vertexPositionNormalColor)
        {
            this.vertexPositionNormalColor = vertexPositionNormalColor;
        }


        public VertexPositionNormalColor[] GetVertexPositionNormalColor() {
            return vertexPositionNormalColor;
        }

        public List<ushort> GetIndices()
        {
            return indices;
        }

        public void SetIndices(List<ushort> indices)
        {
            this.indices = indices;
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

        public void SetUpIndicesAndData(IndexBuffer indexBuffer)
        {
            this.indexBuffer = indexBuffer;
            indexBuffer.SetData<ushort>(indices.ToArray());
        }

        // PROPERTIES
        // --------------------------------------------------------------------------------

        // controls, whether or not an index buffer shall be used for rendering
        public bool IndexedRendering
        {
            get { return indexedRendering; }
            set { indexedRendering = value; }
        }


        public VertexBuffer VertexBuffer
        {
            get { return vertexBuffer; }
        }

        public IndexBuffer IndexBuffer
        {
            get { return indexBuffer; }
        }

    }
}
