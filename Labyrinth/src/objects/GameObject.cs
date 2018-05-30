namespace Labyrinth
{
    /// <summary>
    /// An object in the game space. Serves as a base class for more specific classes.
    /// </summary>
    class GameObject
    {
        private VertexBuffer<ColoredVertex> vBuffer;
        private IndexBuffer iBuffer;

        public VertexBuffer<ColoredVertex> VBuffer { get { return vBuffer; } set { vBuffer = value; } }
        internal IndexBuffer IBuffer { get => iBuffer; set => iBuffer = value; }

        public GameObject()
        {
            vBuffer = new VertexBuffer<ColoredVertex>(ColoredVertex.Size);
            iBuffer = new IndexBuffer();
        }

        /// <summary>
        /// Draw the vertices and indices needed to display the object on the screen
        /// </summary>
        public virtual void Draw()
        {
            // Bind and buffer vertex buffer
            vBuffer.Bind();
            vBuffer.BufferData();

            // Bind and buffer index buffer
            iBuffer.Bind();
            iBuffer.BufferData();

            // Draw indices
            iBuffer.Draw();
        }
    }
}
