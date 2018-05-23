using OpenTK;
using System.Drawing;

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
    }
}
