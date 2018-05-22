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

        public float XAngle { get => xAngle; set => xAngle = value; }
        public float YAngle { get => yAngle; set => yAngle = value; }

        /// <summary>
        /// Initialize board
        /// </summary>
        public Board()
        {
            // This is all tentative
            // Vertex list
            ColoredVertex[] vertices =
            {
                new ColoredVertex(new Vector3(-2, -1, -2.5f), Color.BlanchedAlmond),
                new ColoredVertex(new Vector3(-2, 1, -2.5f), Color.BlanchedAlmond),
                new ColoredVertex(new Vector3(2, 1, -2.5f), Color.BlanchedAlmond),
                new ColoredVertex(new Vector3(2, -1, -2.5f), Color.BlanchedAlmond),
                new ColoredVertex(new Vector3(-2, -1, -1.5f), Color.Brown),
                new ColoredVertex(new Vector3(-2, 1, -1.5f), Color.Brown),
                new ColoredVertex(new Vector3(2, 1, -1.5f), Color.Brown),
                new ColoredVertex(new Vector3(2, -1, -1.5f), Color.Brown)
            };

            // Index list
            uint[] indices = { 0, 1, 2, 3, 7, 1, 5, 4, 7, 6, 2, 4, 0, 1 };

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
            yAngle += 0.05f *(pos2.Y - pos1.Y);
        }
    }
}
