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

            topBound = 1.0f;
            bottomBound = -1.0f;

            frontDepth = -1.5f;
            backDepth = -2.0f;

            xAngleMin = -0.52f;
            yAngleMin = -0.52f;
            xAngleMax = 0.52f;
            yAngleMax = 0.52f;

            // This is all tentative
            // Vertex list
            ColoredVertex[] vertices = BufferGenerator.CubeVertices(
                frontDepth, backDepth, topBound, bottomBound, rightBound, leftBound,
                Color.AliceBlue, Color.AntiqueWhite, Color.Aqua, Color.Bisque, Color.Thistle, Color.SpringGreen);

            // Index list
            uint[] indices = BufferGenerator.CubeIndices();

            // Add lists to buffers
            VBuffer.AddVertices(vertices);
            IBuffer.AddIndices(indices);
        }

        /// <summary>
        /// Calculate tilt angle of board based on two mouse positions
        /// </summary>
        /// <param name="pos1">Previous mouse position</param>
        /// <param name="pos2">Current mouse position</param>
        public void Tilt(Vector2 pos1)
        {
            xAngle += 0.005f * (pos1.X);
            yAngle += 0.005f * (pos1.Y);
            
            if (xAngle > xAngleMax) xAngle = xAngleMax;
            else if (xAngle < xAngleMin) xAngle = xAngleMin;

            if (yAngle > yAngleMax) yAngle = yAngleMax;
            else if (yAngle < yAngleMin) yAngle = yAngleMin;
        }
    }
}
