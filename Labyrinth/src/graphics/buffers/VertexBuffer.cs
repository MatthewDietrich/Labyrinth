// Based on https://github.com/amulware/genericgamedev-opentk-intro

using OpenTK.Graphics.OpenGL;
using System;
using System.Linq;

namespace Labyrinth
{
    /// <summary>
    /// Vertex buffer with methods for binding, buffering, and drawing
    /// </summary>
    /// <typeparam name="TVertex">Type of vertex stored in buffer</typeparam>
    sealed class VertexBuffer<TVertex> : Buffer<TVertex>
        where TVertex : struct
    {
        private readonly int vertexSize;

        public VertexBuffer(int vertexSize)
        {
            this.vertexSize = vertexSize;
        }

        /// <summary>
        /// Bind buffer to GL context
        /// </summary>
        public override void Bind() => GL.BindBuffer(BufferTarget.ArrayBuffer, handle);

        /// <summary>
        /// Put data in buffer
        /// </summary>
        public override void BufferData() => GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertexSize * count), bufferItems, BufferUsageHint.StreamDraw);

        /// <summary>
        /// Draw buffered data
        /// </summary>
        public override void Draw() => GL.DrawArrays(PrimitiveType.Triangles, 0, count);

        public void BufferSubData(int offset, int size)
        {
            TVertex[] subArray = GetSubArray(offset, size);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)offset, size * sizeof(uint), subArray);
        }
    }
}
