// Based on https://github.com/amulware/genericgamedev-opentk-intro

using OpenTK.Graphics.OpenGL;
using System;
using System.Linq;

namespace Labyrinth
{
    /// <summary>
    /// Vertex buffer with methods for binding, buffering, and drawing. Extends <see cref="Buffer{T}"/>.
    /// </summary>
    /// <typeparam name="TVertex">Type of vertex stored in buffer</typeparam>
    sealed class VertexBuffer<TVertex> : Buffer<TVertex>
        where TVertex : struct
    {
        private readonly int vertexSize; // Size of vertrex structure in bytes

        /// <summary>
        /// Create vertexBuffer class
        /// </summary>
        /// <param name="vertexSize">Size of vertes structure in bytes</param>
        public VertexBuffer(int vertexSize)
        {
            this.vertexSize = vertexSize;
        }


        public override void Bind() => GL.BindBuffer(BufferTarget.ArrayBuffer, handle);
        public override void BufferData() => GL.BufferData(BufferTarget.ArrayBuffer, vertexSize * count, bufferItems, BufferUsageHint.StreamDraw);
        public override void Draw() => GL.DrawArrays(PrimitiveType.Triangles, 0, count);

        /// <summary>
        /// Insert a subset of bufferItems into the buffer
        /// </summary>
        /// <param name="offset">Number of elements to offset of beginning of subset</param>
        /// <param name="size">Number of elements in subset</param>
        public override void BufferSubData(int offset, int size)
        {
            TVertex[] subArray = GetSubArray(offset, size + offset); // Retrieve subset of bufferItems

            // Pass data to GL buffer
            GL.BufferSubData(BufferTarget.ArrayBuffer,
                (IntPtr)(offset * vertexSize), // (offset size in bytes) == (number of elements to offset) * (vertex size in bytes)
                size * vertexSize, // (subarray size in bytes) == (number of elements in subarray) * (vertex size in bytes)
                subArray); // Subset of elements to pass to buffer
        }

        public override void Prepare(int size)
        {
            GL.BufferData(BufferTarget.ArrayBuffer, count * vertexSize, (IntPtr)null, BufferUsageHint.StreamDraw);
        }
    }
}
