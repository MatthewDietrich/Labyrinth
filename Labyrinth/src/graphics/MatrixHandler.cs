using OpenTK;

namespace Labyrinth
{
    class MatrixHandler
    {
        private ShaderProgram program;
        private Matrix4Uniform modelMatrix;
        private Matrix4Uniform viewMatrix;
        private Matrix4Uniform projectionMatrix;

        internal Matrix4Uniform ModelMatrix { get => modelMatrix; set => modelMatrix = value; }
        internal Matrix4Uniform ViewMatrix { get => viewMatrix; set => viewMatrix = value; }
        internal Matrix4Uniform ProjectionMatrix { get => projectionMatrix; set => projectionMatrix = value; }

        public MatrixHandler(ShaderProgram program)
        {
            ModelMatrix = new Matrix4Uniform("modelMatrix");
            ViewMatrix = new Matrix4Uniform("viewMatrix");
            ProjectionMatrix = new Matrix4Uniform("projectionMatrix");

            this.program = program;
        }

        /// <summary>
        /// Set all matrices to use the program
        /// </summary>
        public void SetAll()
        {
            ModelMatrix.Set(program);
            ViewMatrix.Set(program);
            ProjectionMatrix.Set(program);
        }

        /// <summary>
        /// Set model matrix to rotation in object space
        /// </summary>
        /// <param name="x">Rotation around X-Axis</param>
        /// <param name="y">Rotation around Y-Axis</param>
        /// <param name="z">Rotation around Z-Axis</param>
        public void rotateModelMatrix(float x, float y, float z)
        {
            ModelMatrix.Matrix *= Matrix4.CreateRotationX(x);
            ModelMatrix.Matrix *= Matrix4.CreateRotationY(y);
            ModelMatrix.Matrix *= Matrix4.CreateRotationZ(z);
        }

        /// <summary>
        /// Set all matrices to default values
        /// </summary>
        public void Default()
        {
            ModelMatrix.Matrix = Matrix4.Identity;
            ViewMatrix.Matrix = Matrix4.Identity;
            ProjectionMatrix.Matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 16f / 9, 0.1f, 100f);
        }
    }
}
