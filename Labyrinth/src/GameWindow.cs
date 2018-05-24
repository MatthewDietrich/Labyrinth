using System;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Labyrinth
{
    sealed class GameWindow : OpenTK.GameWindow
    {
        #region private_members
        private ShaderProgram shaderProgram;
        private VertexArray<ColoredVertex> vertexArray;
        private MatrixHandler matrixHandler;
        private Board board;

        private Vector2 currentMousePos;

        private int halfWidth, halfHeight;

        #endregion

        public GameWindow(int screenWidth, int screenHeight) : base(screenWidth, screenHeight, GraphicsMode.Default, "Labyrinth", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.ForwardCompatible)
        {
            halfWidth = Width / 2;
            halfHeight = Height / 2;
            Console.WriteLine("OpenGL version: " + GL.GetString(StringName.Version));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            board = new Board();

            currentMousePos = new Vector2(0, 0);

            Shader vertexShader = new Shader(ShaderType.VertexShader, File.ReadAllText(@"..\..\shaders\vertex-shader.vs"));
            Shader fragmentShader = new Shader(ShaderType.FragmentShader, File.ReadAllText(@"..\..\shaders\fragment-shader.fs"));

            shaderProgram = new ShaderProgram(vertexShader, fragmentShader);

            vertexArray = new VertexArray<ColoredVertex>(board.VBuffer, shaderProgram,
                new VertexAttribute("vPosition", 3, VertexAttribPointerType.Float, ColoredVertex.Size, 0),
                new VertexAttribute("vColor", 4, VertexAttribPointerType.Float, ColoredVertex.Size, 12));

            matrixHandler = new MatrixHandler(shaderProgram);
            matrixHandler.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 16f / 9, 0.1f, 100f);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // Set values
            GL.ClearColor(Color4.CornflowerBlue);
            matrixHandler.Default(); // Reset default values of matrices

            board.Tilt(currentMousePos);
            matrixHandler.rotateModelMatrix(board.YAngle, board.XAngle, 0);

            // Render frame
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // Clear screen
            shaderProgram.Use(); // Use shader program
            matrixHandler.SetAll(); // Bind matrices to their variables in shader program
            vertexArray.Bind(); // Bind vertex array
            board.Draw(); // Draw

            UnbindAll(); // Unbind everything

            this.SwapBuffers(); // Update buffers
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            currentMousePos = MouseShift(new Vector2(e.Mouse.X, e.Mouse.Y));
        }

        /// <summary>
        /// Unbind program and buffers
        /// </summary>
        private void UnbindAll()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.UseProgram(0);
        }

        private Vector2 MouseShift(Vector2 mousePos)
        {
            Vector2 shiftedMousePos = new Vector2();

            shiftedMousePos.X = -(halfWidth - mousePos.X);
            shiftedMousePos.Y = -(halfHeight - mousePos.Y);

            return shiftedMousePos;
        }
    }
}