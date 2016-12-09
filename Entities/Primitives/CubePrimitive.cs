
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainBase.VertexType;

namespace RainBase.Entities.Primitives
{
    public class CubePrimitive
    {
        private const float DEFAULT_SIZE = 1.0f;
        public static int NUMBER_OF_PRIMITIVES = 12;
        private float width, height, depth;
        private Vector3 position;


        private Vector3[] vertices = new Vector3[8];
        private Vector3[] normals = new Vector3[6];
        private VertexPositionNormalColor[] faceData = new VertexPositionNormalColor[NUMBER_OF_PRIMITIVES * 3];

        private Matrix scaling;
        private Matrix translation;
        private Matrix rotation;

        private Color color;


        public CubePrimitive(Vector3 position, Color color, float size = DEFAULT_SIZE)
        {
            this.position = position;
            this.width = size;
            this.height = size;
            this.depth = size;
            this.scaling = Matrix.CreateScale(size);
            this.translation = Matrix.CreateTranslation(position);
            this.color = color;
            this.rotation = Matrix.Identity;
            SetupVertexAndFaceData();
        }

        public CubePrimitive(Vector3 position, Color color, float width, float height, float depth, Matrix rotation)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.color = color;

            this.scaling = Matrix.CreateScale(new Vector3(width, height, depth));
            this.translation = Matrix.CreateTranslation(position);

            this.rotation = rotation;
            SetupVertexAndFaceData();
        }


        private void SetupVertexAndFaceData()
        {

            vertices[0] = new Vector3(-1, -1, -1);   // BOTTOM FRONT LEFT
            vertices[1] = new Vector3(1, -1, -1);   // BOTTOM FRONT RIGHT
            vertices[2] = new Vector3(1, -1, 1);    // BOTTOM BACK LEFT
            vertices[3] = new Vector3(-1, -1, 1);   // BOTTOM BACK RIGHT

            vertices[4] = new Vector3(-1, 1, -1);  // TOP FRONT LEFT
            vertices[5] = new Vector3(1, 1, -1);   // TOP FRONT RIGHT
            vertices[6] = new Vector3(1, 1, 1);    // TOP BACK LEFT
            vertices[7] = new Vector3(-1, 1, 1);   // TOP BACK RIGHT




            Matrix combined = scaling * rotation  * translation;

            // SCALING, ROTATION & TRANSLATION
            // Applies scaling, rotation and translation to each vertex.
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = Vector3.Transform(vertices[i], combined);
            }

            // BOTTOM FACE

            normals[0] = new Vector3(0f, -1f, 0);
            faceData[0] = new VertexPositionNormalColor(vertices[0], normals[0], color);
            faceData[1] = new VertexPositionNormalColor(vertices[3], normals[0], color);
            faceData[2] = new VertexPositionNormalColor(vertices[1], normals[0], color);
            faceData[3] = new VertexPositionNormalColor(vertices[3], normals[0], color);
            faceData[4] = new VertexPositionNormalColor(vertices[2], normals[0], color);
            faceData[5] = new VertexPositionNormalColor(vertices[1], normals[0], color);

            normals[1] = new Vector3(0f, 0f, -1f);

            // FRONT FACE

            faceData[6] = new VertexPositionNormalColor(vertices[4], normals[1], color);
            faceData[7] = new VertexPositionNormalColor(vertices[0], normals[1], color);
            faceData[8] = new VertexPositionNormalColor(vertices[5], normals[1], color);
            faceData[9] = new VertexPositionNormalColor(vertices[0], normals[1], color);
            faceData[10] = new VertexPositionNormalColor(vertices[1], normals[1], color);
            faceData[11] = new VertexPositionNormalColor(vertices[5], normals[1], color);


            // TOP FACE
            normals[2] = new Vector3(0f, 1f, 0f);

            faceData[12] = new VertexPositionNormalColor(vertices[7], normals[2], color);
            faceData[13] = new VertexPositionNormalColor(vertices[4], normals[2], color);
            faceData[14] = new VertexPositionNormalColor(vertices[6], normals[2], color);
            faceData[15] = new VertexPositionNormalColor(vertices[4], normals[2], color);
            faceData[16] = new VertexPositionNormalColor(vertices[5], normals[2], color);
            faceData[17] = new VertexPositionNormalColor(vertices[6], normals[2], color);


            // RIGHT FACE
            normals[3] = new Vector3(1f, 0f, 0f);

            faceData[18] = new VertexPositionNormalColor(vertices[5], normals[3], color);
            faceData[19] = new VertexPositionNormalColor(vertices[1], normals[3], color);
            faceData[20] = new VertexPositionNormalColor(vertices[6], normals[3], color);
            faceData[21] = new VertexPositionNormalColor(vertices[1], normals[3], color);
            faceData[22] = new VertexPositionNormalColor(vertices[2], normals[3], color);
            faceData[23] = new VertexPositionNormalColor(vertices[6], normals[3], color);


            // LEFT FACE
            normals[4] = new Vector3(-1f, 0f, 0f);

            faceData[24] = new VertexPositionNormalColor(vertices[7], normals[4], color);
            faceData[25] = new VertexPositionNormalColor(vertices[3], normals[4], color);
            faceData[26] = new VertexPositionNormalColor(vertices[4], normals[4], color);
            faceData[27] = new VertexPositionNormalColor(vertices[3], normals[4], color);
            faceData[28] = new VertexPositionNormalColor(vertices[0], normals[4], color);
            faceData[29] = new VertexPositionNormalColor(vertices[4], normals[4], color);

            // BACK FACE
            normals[5] = new Vector3(0f, -1f, 0f);

            faceData[30] = new VertexPositionNormalColor(vertices[6], normals[5], color);
            faceData[31] = new VertexPositionNormalColor(vertices[2], normals[5], color);
            faceData[32] = new VertexPositionNormalColor(vertices[7], normals[5], color);
            faceData[33] = new VertexPositionNormalColor(vertices[2], normals[5], color);
            faceData[34] = new VertexPositionNormalColor(vertices[3], normals[5], color);
            faceData[35] = new VertexPositionNormalColor(vertices[7], normals[5], color);

        }


        public VertexPositionNormalColor[] GetVertexPositionNormalColor()
        {
            return faceData;
        }
    }
}
