using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    class Ball : GameObject
    {
        public Ball()
        {
            ColoredVertex[] vertices = BufferGenerator.RegularPolyhedronVertices(30, 0.05f, Color.Orange);
            uint[] indices = BufferGenerator.RegularPolyhedronIndices(30);
        }

        public override void Draw()
        {
            // Bind and buffer vertex buffer
            VBuffer.Bind();
            VBuffer.BufferData();

            // Bind and buffer index buffer
            IBuffer.Bind();
            IBuffer.BufferData();

            // Draw indices
            IBuffer.Draw(PrimitiveType.TriangleFan);
        }
    }
}
