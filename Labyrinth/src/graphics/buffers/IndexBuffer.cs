using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace Labyrinth
{
    sealed class IndexBuffer : Buffer<uint>
    {
        public override void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle);
        public override void BufferData() => GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(count * sizeof(uint)), bufferItems, BufferUsageHint.StreamDraw);
        public override void Draw() => GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, 0);
    }
}
