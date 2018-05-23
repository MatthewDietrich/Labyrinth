// Based on https://github.com/amulware/genericgamedev-opentk-intro

using OpenTK.Graphics.OpenGL;
using System;

namespace Labyrinth
{
    /// <summary>
    /// Vertex buffer with methods for binding, buffering, and drawing
    /// </summary>
    /// <typeparam name="TVertex">Type of vertex stored in buffer</typeparam>
    sealed class VertexBuffer<TVertex>
        where TVertex : struct
    {
        private readonly int vertexSize;
        private TVertex[] vertices = new TVertex[4];

        private int count;

        private readonly int handle;

        public VertexBuffer(int vertexSize)
        {
            this.vertexSize = vertexSize;
            handle = GL.GenBuffer();
        }

        /// <summary>
        /// Insert a vertex into the buffer
        /// </summary>
        /// <param name="vertex">Vertex object to add</param>
        public void AddVertex (TVertex vertex)
        {
            if (count == vertices.Length)
                Array.Resize(ref vertices, count * 2);
            vertices[count] = vertex;
            count++;
        }

        public void AddVertices(TVertex [] vertexList)
        {
            foreach (TVertex vertex in vertexList)
            {
                if (count == vertices.Length)
                    Array.Resize(ref vertices, count * 2);
                vertices[count] = vertex;
                count++;
            }
        }
        
        /// <summary>
        /// Bind buffer to GL context
        /// </summary>
        public void Bind() => GL.BindBuffer(BufferTarget.ArrayBuffer, handle);

        /// <summary>
        /// Put data in buffer
        /// </summary>
        public void BufferData() => GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertexSize * count), vertices, BufferUsageHint.StreamDraw);

        /// <summary>
        /// Draw buffered data
        /// </summary>
        public void Draw() => GL.DrawArrays(PrimitiveType.Triangles, 0, count);
    }
}
