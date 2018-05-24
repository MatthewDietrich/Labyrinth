using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

namespace Labyrinth
{
    /// <summary>
    /// Handles the creation of a shader from source code and keeps track of its ID in the GL context
    /// </summary>
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

            // Print shader compile information log to Debug
            if (!string.IsNullOrWhiteSpace(info))
                Debug.WriteLine($"GL.CompileShader [{type}] has info log [{info}]");
        }
    }
}
