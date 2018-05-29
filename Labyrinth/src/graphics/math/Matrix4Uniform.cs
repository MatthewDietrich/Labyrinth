// Based on https://github.com/amulware/genericgamedev-opentk-intro

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Labyrinth
{
    /// <summary>
    /// Matrix4 associated with uniform variable in shader program
    /// </summary>
    sealed class Matrix4Uniform
    {
        private readonly string name;
        private Matrix4 matrix;
        public Matrix4 Matrix { get { return matrix; } set { matrix = value; } }

        /// <summary>
        /// Create Uniform Matrix4
        /// </summary>
        /// <param name="name">Name of matrix in program</param>
        public Matrix4Uniform(string name)
        {
            this.name = name;
            matrix = Matrix4.Identity; // Initially set to identity matrix so program still works before anything is explicitly initialized
        }

        /// <summary>
        /// Associate matrix with its univorm name in the shader program.
        /// </summary>
        /// <param name="program">Shader program</param>
        public void Set (ShaderProgram program)
        {
            var uniformLocation = program.GetUniformLocation(name);
            GL.UniformMatrix4(uniformLocation, false, ref matrix);
        }
    }
}
