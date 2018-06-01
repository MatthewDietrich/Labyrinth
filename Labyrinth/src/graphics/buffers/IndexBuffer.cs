using OpenTK.Graphics.OpenGL;
using System;

namespace Labyrinth
{
    /// <summary>
    /// Index buffer with methods for binding, buffering, and drawing. Extends <see cref="Buffer{T}"/>.
    /// </summary>
    sealed class IndexBuffer : Buffer<uint>
    {
        public override void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle);
        public override void BufferData() => GL.BufferData(BufferTarget.ElementArrayBuffer, count * sizeof(uint), bufferItems, BufferUsageHint.StreamDraw);

        public override void Draw() => GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, 0);
        public void Draw(int num) => GL.DrawElements(PrimitiveType.Triangles, num, DrawElementsType.UnsignedInt, 0);
        public void Draw(PrimitiveType primitiveType) => GL.DrawElements(primitiveType, count, DrawElementsType.UnsignedInt, 0);

        /// <summary>
        /// Insert a subset of bufferItems into the buffer
        /// </summary>
        /// <param name="offset">Number of elements to offset of beginning of subset</param>
        /// <param name="size">Number of elements in subset</param>
        public override void BufferSubData(int offset, int size)
        {
            uint[] subArray = GetSubArray(offset, size + offset); // Retrieve subset of bufferItems

            // Pass data to GL buffer
            GL.BufferSubData(BufferTarget.ElementArrayBuffer,
                (IntPtr)(offset * sizeof(uint)), // (offset size in bytes) == (number of elements to offset) * (uint size in bytes)
                size * sizeof(uint), // (subarray size in bytes) == (number of elements in subarray) * (uint size in bytes)
                subArray); // Subset of elements to pass to buffer
        }

        public override void Prepare(int size)
        {
            GL.BufferData(BufferTarget.ElementArrayBuffer, count * sizeof(uint), (IntPtr)null, BufferUsageHint.StreamDraw);
        }
    }
}
