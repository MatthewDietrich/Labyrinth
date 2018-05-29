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

        private readonly float width;
        private readonly float height;

        public float XAngle { get => xAngle; set => xAngle = value; }
        public float YAngle { get => yAngle; set => yAngle = value; }

        /// <summary>
        /// Initialize board
        /// </summary>
        /// <param name="width">Width of the board in object coordinates</param>
        /// <param name="height">Height of the board in object coordinates</param>
        public Board(float width, float height)
        {
            // Store dimensions
            this.width = width;
            this.height = height;

            // Calculate bounds of board
            leftBound = -width / 2;
            rightBound = width / 2;
            topBound = -height / 2;
            bottomBound = height / 2;

            // Magic numbers
            frontDepth = -1.5f;
            backDepth = -2.0f;
            xAngleMin = -0.52f;
            yAngleMin = -0.52f;
            xAngleMax = 0.52f;
            yAngleMax = 0.52f;

            // Vertex list
            ColoredVertex[] vertices = BufferGenerator.CubeVertices(
                frontDepth, backDepth, topBound, bottomBound, rightBound, leftBound, Color.Magenta);

            // Index list
            uint[] indices = BufferGenerator.CubeIndices();

            // Add lists to buffers
            VBuffer.AddVertices(vertices);
            IBuffer.AddIndices(indices);
        }

        /// <summary>
        /// Calculate tilt angle of board based on mouse positions
        /// </summary>
        /// <param name="mousePosition">Distance, in pixels, of the mouse cursor from the center of the screen</param>
        public void Tilt(Vector2 mousePosition)
        {
            // Calculate new angle
            xAngle = 0.005f * (mousePosition.X);
            yAngle = 0.005f * (mousePosition.Y);
            
            // Check X bounds
            if (xAngle > xAngleMax) xAngle = xAngleMax;
            else if (xAngle < xAngleMin) xAngle = xAngleMin;

            // Check Y bounds
            if (yAngle > yAngleMax) yAngle = yAngleMax;
            else if (yAngle < yAngleMin) yAngle = yAngleMin;
        }
    }
}
