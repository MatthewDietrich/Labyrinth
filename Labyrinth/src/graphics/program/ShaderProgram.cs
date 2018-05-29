using OpenTK.Graphics.OpenGL;

namespace Labyrinth
{
    sealed class ShaderProgram
    {
        private readonly int handle;

        public ShaderProgram(params Shader[] shaders)
        {
            handle = GL.CreateProgram();

            foreach (var shader in shaders)
                GL.AttachShader(handle, shader.Handle);

            GL.LinkProgram(handle);

            foreach (var shader in shaders)
                GL.DetachShader(handle, shader.Handle);
        }

        public void Use() => GL.UseProgram(handle);
        public int GetAttributeLocation(string name) => GL.GetAttribLocation(handle, name);
        public int GetUniformLocation(string name) => GL.GetUniformLocation(handle, name);
    }
}
