using System.Collections.Generic;
using System.Drawing;
using OpenTK;

namespace Labyrinth
{
    /// <summary>
    /// The board on which the ball rolls. Controlled by mouse movement
    /// </summary>
    class Board : GameObject
    {
        private float xAngle;
        private float yAngle;
        private readonly float xAngleMin;
        private readonly float xAngleMax;
        private readonly float yAngleMin;
        private readonly float yAngleMax;

        private readonly float leftBound;
        private readonly float rightBound;
        private readonly float topBound;
        private readonly float bottomBound;
        private readonly float frontDepth;
        private readonly float backDepth;

        public float XAngle { get => xAngle; set => xAngle = value; }
        public float YAngle { get => yAngle; set => yAngle = value; }

        /// <summary>
        /// Initialize board
        /// </summary>
        public Board()
        {
            leftBound = -2.0f;
            rightBound = 2.0f;

            topBound = -1.0f;
            bottomBound = -1.0f;

            frontDepth = -1.5f;
            backDepth = -2.0f;

            xAngleMin = -0.22f;
            yAngleMin = -0.22f;
            xAngleMax = 0.22f;
            yAngleMax = 0.22f;

            // This is all tentative
            // Vertex list
            ColoredVertex[] vertices =
            {
                // front
                new ColoredVertex(new Vector3(leftBound, bottomBound, frontDepth), Color.BlanchedAlmond),
                new ColoredVertex(new Vector3(rightBound, bottomBound, frontDepth), Color.BlanchedAlmond),
                new ColoredVertex(new Vector3(rightBound, topBound, frontDepth), Color.BlanchedAlmond),
                new ColoredVertex(new Vector3(leftBound, topBound, frontDepth), Color.BlanchedAlmond),

                // back
                new ColoredVertex(new Vector3(leftBound, bottomBound, backDepth), Color.Brown),
                new ColoredVertex(new Vector3(leftBound, topBound, backDepth), Color.Brown),
                new ColoredVertex(new Vector3(rightBound, topBound, backDepth), Color.Brown),
                new ColoredVertex(new Vector3(rightBound, bottomBound, backDepth), Color.Brown),

                // top
                new ColoredVertex(new Vector3(leftBound, topBound, backDepth), Color.Blue),
                new ColoredVertex(new Vector3(leftBound, topBound, frontDepth), Color.Blue),
                new ColoredVertex(new Vector3(rightBound, topBound, frontDepth), Color.Blue),
                new ColoredVertex(new Vector3(rightBound, topBound, backDepth), Color.Blue),

                // bottom
                new ColoredVertex(new Vector3(leftBound, bottomBound, backDepth), Color.MediumOrchid),
                new ColoredVertex(new Vector3(rightBound, bottomBound, backDepth), Color.MediumOrchid),
                new ColoredVertex(new Vector3(rightBound, bottomBound, frontDepth), Color.MediumOrchid),
                new ColoredVertex(new Vector3(topBound, bottomBound, frontDepth), Color.MediumOrchid),

                // right
                new ColoredVertex(new Vector3(rightBound, topBound, backDepth), Color.Gray),
                new ColoredVertex(new Vector3(rightBound, topBound, frontDepth), Color.Gray),
                new ColoredVertex(new Vector3(rightBound, bottomBound, frontDepth), Color.Gray),
                new ColoredVertex(new Vector3(rightBound, bottomBound, backDepth), Color.Gray),

                // left
                new ColoredVertex(new Vector3(leftBound, bottomBound, backDepth), Color.Magenta),
                new ColoredVertex(new Vector3(leftBound, bottomBound, frontDepth), Color.Magenta),
                new ColoredVertex(new Vector3(leftBound, topBound, frontDepth), Color.Magenta),
                new ColoredVertex(new Vector3(leftBound, topBound, backDepth), Color.Magenta)
            };

            // Index list
            uint[] indices = {
                0, 1, 2, 0, 2, 3,    // front
                4, 5, 6, 4, 6, 7,    // back
                8, 9, 10, 8, 10, 11,   // top
                12, 13, 14, 12, 14, 15,   // bottom
                16, 17, 18, 16, 18, 19,   // right
                20, 21, 22, 20, 22, 23   // left
            };

            // Add lists to buffers
            VBuffer.AddVertices(vertices);
            IBuffer.AddIndices(indices);
        }

        /// <summary>
        /// Calculate tilt angle of board based on two mouse positions
        /// </summary>
        /// <param name="pos1">Previous mouse position</param>
        /// <param name="pos2">Current mouse position</param>
        public void Tilt(Vector2 pos1, Vector2 pos2)
        {
            xAngle += 0.05f * (pos2.X - pos1.X);
            yAngle += 0.05f * (pos2.Y - pos1.Y);
            
            if (xAngle > xAngleMax) xAngle = xAngleMax;
            else if (xAngle < xAngleMin) xAngle = xAngleMin;

            if (yAngle > yAngleMax) yAngle = yAngleMax;
            else if (yAngle < yAngleMin) yAngle = yAngleMin;
        }
    }
}
