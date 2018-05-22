// Based on https://github.com/amulware/genericgamedev-opentk-intro

using OpenTK.Graphics.OpenGL;

namespace Labyrinth
{
    sealed class VertexArray<TVertex>
        where TVertex : struct
    {
        private readonly int handle;

        public VertexArray(VertexBuffer<TVertex> vertexBuffer, ShaderProgram program, params VertexAttribute[] attributes)
        {
            GL.GenVertexArrays(1, out handle);
            Bind();
            vertexBuffer.Bind();
            
            foreach (var attribute in attributes)
                attribute.Set(program);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Bind()
        {
            GL.BindVertexArray(handle);
        }
    }
}
