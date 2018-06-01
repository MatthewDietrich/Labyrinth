using System.Collections;
using System.Collections.Generic;

namespace Labyrinth
{
    /// <summary>
    /// List of objects in the game world. Handles placing them in buffers, as well as drawing them.
    /// </summary>
    class GameObjectList : IEnumerable<GameObject>
    {
        private List<GameObject> gameObjects;

        public Board CurrentBoard { get; private set; }

        public VertexBuffer<ColoredVertex> VBuffer { get; private set; }
        public IndexBuffer IBuffer { get; private set; }

        private int vertexOffset; // Position in vertex buffer to place first vertex of next object
        private int indexOffset; // Position in index buffer to place first index of next object

        public GameObjectList()
        {
            gameObjects = new List<GameObject>();

            VBuffer = new VertexBuffer<ColoredVertex>(ColoredVertex.Size);
            IBuffer = new IndexBuffer();

            vertexOffset = 0;
            indexOffset = 0;
        }
        
        /// <summary>
        /// Add an object to the game object list
        /// </summary>
        /// <param name="gameObject">Object to add</param>
        public void Add(GameObject gameObject)
        {
            gameObjects.Add(gameObject);

            gameObject.VertexOffset = vertexOffset;
            gameObject.IndexOffset = indexOffset;

            VBuffer.Add(gameObject.Vertices);
            IBuffer.Add(gameObject.Indices);

            // Keep track of most recent board added
            if (gameObject.GetType() == typeof(Board))
                CurrentBoard = (Board)gameObject;

            vertexOffset += gameObject.Vertices.Length;
            indexOffset += gameObject.Indices.Length;
        }

        public void PrepareBuffers()
        {
            int totalVertices = 0;
            int totalIndices = 0;
            foreach (var gameObject in gameObjects)
            {
                totalVertices += gameObject.Vertices.Length;
                totalIndices += gameObject.Vertices.Length;
            }

            VBuffer.Prepare(totalVertices);
            IBuffer.Prepare(totalVertices);
        }

        /// <summary>
        /// Draw all objects in game object list
        /// </summary>
        public void Draw()
        {
            // Iterate through list of objects, drawing each one indifidually
            foreach (var gameObject in gameObjects)
            {
                VBuffer.Bind(); // Bind vertex buffer
                VBuffer.BufferSubData(gameObject.VertexOffset, gameObject.Vertices.Length); // Put vertices in vertex buffer

                IBuffer.Bind(); // Bind index buffer
                IBuffer.BufferSubData(gameObject.IndexOffset, gameObject.Indices.Length); // Put vertices in index buffer

                IBuffer.Draw(gameObject.Indices.Length); // Draw objects by indices
            }
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            foreach (var gameObject in gameObjects)
                yield return gameObject;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
