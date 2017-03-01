#region File Description
//-----------------------------------------------------------------------------
// SpherePrimitive.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainBase.VertexType;
#endregion

namespace RainBase.Entities.Primitives
{
    /// <summary>
    /// Geometric primitive class for drawing spheres.
    /// 
    /// This class is borrowed from the Primitives3D sample.
    /// </summary>
    public class SpherePrimitive : GeometricPrimitive
    {

        private Vector3 position;

        private Matrix translation, scaling, rotation;
        private Color color;
        private BoundingBox boundingBox;
        private Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        private Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        /// <summary>
        /// Constructs a new sphere primitive, using default settings.
        /// </summary>
        public SpherePrimitive()
            : this(new Vector3(0,0,0), Color.White, 1, 16)
        {
        }


        /// <summary>
        /// Constructs a new sphere primitive,
        /// with the specified size and tessellation level.
        /// </summary>
        public SpherePrimitive(Vector3 position, Color color, float diameter, int tessellation)
        {
            this.position = position;
            this.color = color;
            this.scaling = Matrix.Identity;
            this.rotation = Matrix.Identity;
            this.translation = Matrix.CreateTranslation(position);
            if (tessellation < 3)
                throw new ArgumentOutOfRangeException("tessellation");

            int verticalSegments = tessellation;
            int horizontalSegments = tessellation * 2;

            Matrix combined = scaling * rotation * translation;

            float radius = diameter / 2;

            // Start with a single vertex at the bottom of the sphere.
            AddVertex(Vector3.Transform(Vector3.Down * radius, combined), Vector3.Down,color);

            // Create rings of vertices at progressively higher latitudes.
            for (int i = 0; i < verticalSegments - 1; i++)
            {
                float latitude = ((i + 1) * MathHelper.Pi / verticalSegments) - MathHelper.PiOver2;

                float dy = (float)Math.Sin(latitude);
                float dxz = (float)Math.Cos(latitude);

                // Create a single ring of vertices at this latitude.
                for (int j = 0; j < horizontalSegments; j++)
                {
                    float longitude = j * MathHelper.TwoPi / horizontalSegments;

                    float dx = (float)Math.Cos(longitude) * dxz;
                    float dz = (float)Math.Sin(longitude) * dxz;

                    Vector3 normal = new Vector3(dx, dy, dz);

                    AddVertex(Vector3.Transform(normal * radius, combined), normal, color);
                }
            }

            // Finish with a single vertex at the top of the sphere.
            AddVertex(Vector3.Transform(Vector3.Up * radius, combined), Vector3.Up, color);


            foreach (VertexPositionNormalColor vpc in vertices)
            {
                min = Vector3.Min(min, vpc.Position);
                max = Vector3.Max(max, vpc.Position);
            }

            boundingBox = new BoundingBox(min, max);

            // Create a fan connecting the bottom vertex to the bottom latitude ring.
            for (int i = 0; i < horizontalSegments; i++)
            {
                AddIndex(0);
                AddIndex(1 + (i + 1) % horizontalSegments);
                AddIndex(1 + i);
            }

            // Fill the sphere body with triangles joining each pair of latitude rings.
            for (int i = 0; i < verticalSegments - 2; i++)
            {
                for (int j = 0; j < horizontalSegments; j++)
                {
                    int nextI = i + 1;
                    int nextJ = (j + 1) % horizontalSegments;

                    AddIndex(1 + i * horizontalSegments + j);
                    AddIndex(1 + i * horizontalSegments + nextJ);
                    AddIndex(1 + nextI * horizontalSegments + j);

                    AddIndex(1 + i * horizontalSegments + nextJ);
                    AddIndex(1 + nextI * horizontalSegments + nextJ);
                    AddIndex(1 + nextI * horizontalSegments + j);
                }
            }

            // Create a fan connecting the top vertex to the top latitude ring.
            for (int i = 0; i < horizontalSegments; i++)
            {
                AddIndex(CurrentVertex - 1);
                AddIndex(CurrentVertex - 2 - (i + 1) % horizontalSegments);
                AddIndex(CurrentVertex - 2 - i);
            }

           

        }

        public BoundingBox GetBoundingBox()
        {
            return boundingBox;
        }
    }
}
