using OpenTK.Graphics.OpenGL;
using System;

namespace Labyrinth
{
    sealed class IndexBuffer : Buffer<uint>
    {
        public override void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle);
        public override void BufferData() => GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(count * sizeof(uint)), bufferItems, BufferUsageHint.StreamDraw);

        public override void Draw() => GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, 0);
        public void Draw(PrimitiveType primitiveType) => GL.DrawElements(primitiveType, count, DrawElementsType.UnsignedInt, 0);

        public void BufferSubData(int offset, int size)
        {
            uint[] subArray = GetSubArray(offset, size + offset);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)(offset*sizeof(uint)), size * sizeof(uint), subArray);
            System.Diagnostics.Debug.Print("Buffered: o:{0}, s:{1}", (IntPtr)offset, size);
        }
    }
}
