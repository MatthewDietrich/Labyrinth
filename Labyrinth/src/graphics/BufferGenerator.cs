using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Labyrinth
{
    class BufferGenerator
    {
        public static ColoredVertex[] CubeVertices(float frontDepth, float backDepth, float topBound, float bottomBound, float rightBound, float leftBound,
            Color frontColor, Color backColor, Color topColor, Color bottomColor, Color rightColor, Color leftColor)
        {
            ColoredVertex[] vertices =
            {
                // front
                new ColoredVertex(new Vector3(leftBound, bottomBound, frontDepth), frontColor),
                new ColoredVertex(new Vector3(rightBound, bottomBound, frontDepth), frontColor),
                new ColoredVertex(new Vector3(rightBound, topBound, frontDepth), frontColor),
                new ColoredVertex(new Vector3(leftBound, topBound, frontDepth), frontColor),

                // back
                new ColoredVertex(new Vector3(leftBound, bottomBound, backDepth), backColor),
                new ColoredVertex(new Vector3(leftBound, topBound, backDepth), backColor),
                new ColoredVertex(new Vector3(rightBound, topBound, backDepth), backColor),
                new ColoredVertex(new Vector3(rightBound, bottomBound, backDepth), backColor),

                // top
                new ColoredVertex(new Vector3(leftBound, topBound, backDepth), topColor),
                new ColoredVertex(new Vector3(leftBound, topBound, frontDepth), topColor),
                new ColoredVertex(new Vector3(rightBound, topBound, frontDepth), topColor),
                new ColoredVertex(new Vector3(rightBound, topBound, backDepth), topColor),

                // bottom
                new ColoredVertex(new Vector3(leftBound, bottomBound, backDepth), bottomColor),
                new ColoredVertex(new Vector3(rightBound, bottomBound, backDepth), bottomColor),
                new ColoredVertex(new Vector3(rightBound, bottomBound, frontDepth), bottomColor),
                new ColoredVertex(new Vector3(leftBound, bottomBound, frontDepth), bottomColor),

                // right
                new ColoredVertex(new Vector3(rightBound, topBound, backDepth), rightColor),
                new ColoredVertex(new Vector3(rightBound, topBound, frontDepth), rightColor),
                new ColoredVertex(new Vector3(rightBound, bottomBound, frontDepth), rightColor),
                new ColoredVertex(new Vector3(rightBound, bottomBound, backDepth), rightColor),

                // left
                new ColoredVertex(new Vector3(leftBound, bottomBound, backDepth), leftColor),
                new ColoredVertex(new Vector3(leftBound, bottomBound, frontDepth), leftColor),
                new ColoredVertex(new Vector3(leftBound, topBound, frontDepth), leftColor),
                new ColoredVertex(new Vector3(leftBound, topBound, backDepth), leftColor)
            };

            return vertices;
        }

        public static uint[] CubeIndices()
        {
            uint[] indices =
                { 0, 1, 2, 0, 2, 3,    // front
                4, 5, 6, 4, 6, 7,    // back
                8, 9, 10, 8, 10, 11,   // top
                12, 13, 14, 12, 14, 15,   // bottom
                16, 17, 18, 16, 18, 19,   // right
                20, 21, 22, 20, 22, 23 };   // left

            return indices;
        }

        /// <summary>
        /// Generate regular polyhedron.
        /// Algorithm stolen from https://stackoverflow.com/questions/36000898
        /// </summary>
        /// <param name="segments">Number of segments to divide the polyhedron into</param>
        /// <returns>List of vertices</returns>
        public static ColoredVertex[] RegularPolyhedronVertices(int segments, Color color)
        {
            List<ColoredVertex> vertices = new List<ColoredVertex>();

            // Angles
            float theta1 = 0.0f;
            float theta2 = 0.0f;
            float phi1 = 0.0f;
            float phi2 = 0.0f;

            // Cosines
            float cosTheta1 = 0.0f;
            float cosTheta2 = 0.0f;
            float cosPhi1 = 0.0f;
            float cosPhi2 = 0.0f;

            // Sines
            float sinTheta1 = 0.0f;
            float sinTheta2 = 0.0f;
            float sinPhi1 = 0.0f;
            float sinPhi2 = 0.0f;

            for (float latitude = 0; latitude < segments; latitude++)
            {
                phi1 = MathHelper.Pi * latitude / segments;
                phi2 = MathHelper.Pi * (latitude + 1.0f) / segments;

                cosPhi1 = (float)Math.Cos(phi1);
                cosPhi2 = (float)Math.Cos(phi2);
                sinPhi1 = (float)Math.Sin(phi1);
                sinPhi2 = (float)Math.Sin(phi2);

                for (float longitude = 0; longitude < segments; longitude++)
                {
                    theta1 = MathHelper.TwoPi * longitude / segments;
                    theta2 = MathHelper.TwoPi * (longitude + 1.0f) / segments;

                    cosTheta1 = (float)Math.Cos(theta1);
                    cosTheta2 = (float)Math.Cos(theta2);
                    sinTheta1 = (float)Math.Sin(theta1);
                    sinTheta2 = (float)Math.Sin(theta2);

                    vertices.Add(new ColoredVertex(new Vector3(cosTheta1 * sinPhi1, sinTheta1 * sinPhi1, cosPhi1), color));
                    vertices.Add(new ColoredVertex(new Vector3(cosTheta1 * sinPhi2, sinTheta1 * sinPhi2, cosPhi2), color));
                    vertices.Add(new ColoredVertex(new Vector3(cosTheta2 * sinPhi2, sinTheta2 * sinPhi2, cosPhi2), color));
                    vertices.Add(new ColoredVertex(new Vector3(cosTheta2 * sinPhi1, sinTheta2 * sinPhi1, cosPhi1), color));
                }
            }

            return vertices.ToArray();
        }

        /// <summary>
        /// Generate index list for regular polyhedron function.
        /// </summary>
        /// <param name="segments">Number of segments to divide the polyhedron into</param>
        /// <returns>An array with range 0 to (2 * segments) ^ 2 because the regular polyhedrom algorithm isn't designed to be used with indices</returns>
        public static uint[] RegularPolyhedronIndices(int segments)
        {
            List<uint> indices = new List<uint>();

            for (uint i = 0; i < Math.Pow(2.0 * segments, 2.0); i++)
                indices.Add(i);

            return indices.ToArray();
        }
    }
}
