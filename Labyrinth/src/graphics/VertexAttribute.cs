using OpenTK.Graphics.OpenGL;

namespace Labyrinth
{
    sealed class VertexAttribute
    {
        private readonly string name;
        private readonly int size;
        private readonly VertexAttribPointerType type;
        private readonly int stride;
        private readonly int offset;
        private readonly bool normalize;

        public VertexAttribute(string name, int size, VertexAttribPointerType type, int stride, int offset, bool normalize = false)
        {
            this.name = name;
            this.size = size;
            this.type = type;
            this.stride = stride;
            this.offset = offset;
            this.normalize = normalize;
        }

        public void Set(ShaderProgram program)
        {
            int index = program.GetAttributeLocation(name);

            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, size, type, normalize, stride, offset);
        }
    }
}
