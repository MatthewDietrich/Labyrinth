using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

namespace Labyrinth
{
    sealed class Shader
    {
        private readonly int handle;

        public int Handle { get { return handle; } }

        public Shader(ShaderType type, string code)
        {
            handle = GL.CreateShader(type);
            GL.ShaderSource(handle, code);
            GL.CompileShader(handle);

            string info = GL.GetShaderInfoLog(handle);
            if (!string.IsNullOrWhiteSpace(info))
                Debug.WriteLine($"GL.CompileShader [{type}] has info log [{info}]");
        }
    }
}
