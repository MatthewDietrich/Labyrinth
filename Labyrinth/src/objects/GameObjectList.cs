using System.Collections;
using System.Collections.Generic;

namespace Labyrinth
{
    class GameObjectList : IEnumerable<GameObject>
    {
        private List<GameObject> gameObjects;
        private Board currentBoard;

        public Board CurrentBoard { get { return currentBoard; } }

        public VertexBuffer<ColoredVertex> VBuffer { get; private set; }
        public IndexBuffer IBuffer { get; private set; }

        private int vertexOffset;
        private int indexOffset;

        public GameObjectList()
        {
            gameObjects = new List<GameObject>();
            VBuffer = new VertexBuffer<ColoredVertex>(ColoredVertex.Size);
            IBuffer = new IndexBuffer();
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
            {
                currentBoard = (Board)gameObject;
            }

            vertexOffset += gameObject.Vertices.Length;
            indexOffset += gameObject.Indices.Length;
        }

        /// <summary>
        /// Draw all objects in game object list
        /// </summary>
        public void Draw()
        {
            foreach (var gameObject in gameObjects)
            {
                VBuffer.Bind();
                VBuffer.BufferSubData(gameObject.VertexOffset, gameObject.Vertices.Length);

                IBuffer.Bind();
                IBuffer.BufferSubData(gameObject.IndexOffset, gameObject.Indices.Length);

                IBuffer.Draw();
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
