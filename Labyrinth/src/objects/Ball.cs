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
            Vertices = BufferGenerator.RegularPolyhedronVertices(30, 0.05f, Color.Orange);
            Indices = BufferGenerator.RegularPolyhedronIndices(30);
        }
    }
}
