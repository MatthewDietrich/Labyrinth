using OpenTK;

namespace Labyrinth
{
    sealed class MatrixHandler
    {
        private ShaderProgram program;
        private Matrix4 modelMatrix;
        private Matrix4 viewMatrix;
        private Matrix4 projectionMatrix;
        private Matrix4Uniform modelViewProjectionMatrix;

        internal Matrix4 ModelMatrix { get => modelMatrix; set => modelMatrix = value; }
        internal Matrix4 ViewMatrix { get => viewMatrix; set => viewMatrix = value; }
        internal Matrix4 ProjectionMatrix { get => projectionMatrix; set => projectionMatrix = value; }

        public MatrixHandler(ShaderProgram program)
        {
            ModelMatrix = new Matrix4();
            ViewMatrix = new Matrix4();
            ProjectionMatrix = new Matrix4();

            modelViewProjectionMatrix = new Matrix4Uniform("modelViewProjectionMatrix");

            this.program = program;
        }

        /// <summary>
        /// Set all matrices to use the program
        /// </summary>
        public void SetAll()
        {
            modelViewProjectionMatrix.Matrix = ModelMatrix * viewMatrix * projectionMatrix;
            modelViewProjectionMatrix.Set(program);
        }

        /// <summary>
        /// Set model matrix to rotation in object space
        /// </summary>
        /// <param name="x">Rotation around X-Axis</param>
        /// <param name="y">Rotation around Y-Axis</param>
        /// <param name="z">Rotation around Z-Axis</param>
        public void rotateModelMatrix(float x, float y, float z)
        {
            ModelMatrix *= Matrix4.CreateRotationX(x);
            ModelMatrix *= Matrix4.CreateRotationY(y);
            ModelMatrix *= Matrix4.CreateRotationZ(z);
        }

        /// <summary>
        /// Set all matrices to default values
        /// </summary>
        public void Default()
        {
            // The model matrix is defined by objects, so its default is the identity matrix
            ModelMatrix = Matrix4.Identity;

            // Look at the middle of object space
            ViewMatrix = Matrix4.LookAt(
                eye: new Vector3(0, 0, 0.1f), // Position of eye in object space
                target: new Vector3(0, 0, -100), // Point in object space to look at
                up: new Vector3(0, 1, 0)); // Which way is up?

            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                fovy: MathHelper.PiOver2,
                aspect: 16f / 9, // Aspect ratio. Probably shouldn't be hardcoded but I'll worry about that later
                zNear: 0.001f, // Nearest visible depth
                zFar: 100f); // Farthest visible depth
        }
    }
}
