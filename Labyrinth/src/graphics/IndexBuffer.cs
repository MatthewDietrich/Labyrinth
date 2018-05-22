using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace Labyrinth
{
    sealed class IndexBuffer
    {
        private int count;
        private uint[] indices = new uint[4];
        private readonly int handle;

        public IndexBuffer()
        {
            handle = GL.GenBuffer();
        }

        public void AddIndex (uint index)
        {
            if (count == indices.Length)
                Array.Resize(ref indices, count * 2);
            indices[count] = index;
            count++;
        }

        public void AddIndices (uint[] indexList)
        {
            foreach (uint index in indexList)
            {
                if (count == indices.Length)
                    Array.Resize(ref indices, count * 2);
                indices[count] = index;
                count++;
            }
        }

        public void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle);
        public void BufferData() => GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(count * sizeof(uint)), indices, BufferUsageHint.StreamDraw);
        public void Draw() => GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, 0);
    }
}
