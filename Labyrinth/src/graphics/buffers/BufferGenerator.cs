using OpenTK;
using System.Drawing;

namespace Labyrinth
{
    class BufferGenerator
    {
        public static ColoredVertex[] CubeVertices(float frontDepth, float backDepth, float topBound, float bottomBound, float rightBound, float leftBound,
            Color color)
        {
            ColoredVertex[] vertices =
           {
                // front
                new ColoredVertex(new Vector3(leftBound, bottomBound, frontDepth), color),
                new ColoredVertex(new Vector3(rightBound, bottomBound, frontDepth), color),
                new ColoredVertex(new Vector3(rightBound, topBound, frontDepth), color),
                new ColoredVertex(new Vector3(leftBound, topBound, frontDepth), color),

                // back
                new ColoredVertex(new Vector3(leftBound, bottomBound, backDepth), color),
                new ColoredVertex(new Vector3(leftBound, topBound, backDepth), color),
                new ColoredVertex(new Vector3(rightBound, topBound, backDepth), color),
                new ColoredVertex(new Vector3(rightBound, bottomBound, backDepth), color),

                // top
                new ColoredVertex(new Vector3(leftBound, topBound, backDepth), color),
                new ColoredVertex(new Vector3(leftBound, topBound, frontDepth), color),
                new ColoredVertex(new Vector3(rightBound, topBound, frontDepth), color),
                new ColoredVertex(new Vector3(rightBound, topBound, backDepth), color),

                // bottom
                new ColoredVertex(new Vector3(leftBound, bottomBound, backDepth), color),
                new ColoredVertex(new Vector3(rightBound, bottomBound, backDepth), color),
                new ColoredVertex(new Vector3(rightBound, bottomBound, frontDepth), color),
                new ColoredVertex(new Vector3(leftBound, bottomBound, frontDepth), color),

                // right
                new ColoredVertex(new Vector3(rightBound, topBound, backDepth), color),
                new ColoredVertex(new Vector3(rightBound, topBound, frontDepth), color),
                new ColoredVertex(new Vector3(rightBound, bottomBound, frontDepth), color),
                new ColoredVertex(new Vector3(rightBound, bottomBound, backDepth), color),

                // left
                new ColoredVertex(new Vector3(leftBound, bottomBound, backDepth), color),
                new ColoredVertex(new Vector3(leftBound, bottomBound, frontDepth), color),
                new ColoredVertex(new Vector3(leftBound, topBound, frontDepth), color),
                new ColoredVertex(new Vector3(leftBound, topBound, backDepth), color)
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
    }
}
