using System;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Labyrinth
{
    /// <summary>
    /// Stores all information about the window in which the game runs. Handles input and frame rendering
    /// </summary>
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

            board = new Board(4.0f, 2.0f);

            // Read shader files and store them in the correct shader objects. Then initialize the shader program
            Shader vertexShader = new Shader(ShaderType.VertexShader, File.ReadAllText(@"..\..\shaders\vertex-shader.vs"));
            Shader fragmentShader = new Shader(ShaderType.FragmentShader, File.ReadAllText(@"..\..\shaders\fragment-shader.fs"));
            shaderProgram = new ShaderProgram(vertexShader, fragmentShader);
            
            //Store attributes from vertex shader in vertex array object
            vertexArray = new VertexArray<ColoredVertex>(board.VBuffer, shaderProgram,
                new VertexAttribute("vPosition", 3, VertexAttribPointerType.Float, ColoredVertex.Size, 0),
                new VertexAttribute("vColor", 4, VertexAttribPointerType.Float, ColoredVertex.Size, 12));

            matrixHandler = new MatrixHandler(shaderProgram);
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

            // Calculate angle of board
            board.Tilt(currentMousePos);
            matrixHandler.rotateModelMatrix(board.YAngle, board.XAngle, 0);

            // Draw frame
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // Clear screen
            shaderProgram.Use(); // Use shader program
            matrixHandler.SetAll(); // Bind matrices to their variables in shader program
            vertexArray.Bind(); // Bind vertex array
            board.Draw(); // Draw board

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

        /// <summary>
        /// Shift the origin of a given mouse position from the top-left of the screen to the center of the screen
        /// </summary>
        /// <param name="mousePos">Window coordinates of mouse cursor relative to origin at top-left of screen</param>
        /// <returns>Window coordinates of mouse cursor relative to origin at center of screen</returns>
        private Vector2 MouseShift(Vector2 mousePos)
        {
            return new Vector2(-(halfWidth - mousePos.X), -(halfHeight - mousePos.Y));
        }
    }
}