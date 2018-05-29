using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    class GameObjectList : IEnumerable<GameObject>
    {
        private List<GameObject> gameObjects;
        private Board currentBoard;

        public Board CurrentBoard { get { return currentBoard; } }

        public GameObjectList()
        {
            gameObjects = new List<GameObject>();
        }
        
        /// <summary>
        /// Add an object to the game object list
        /// </summary>
        /// <param name="gameObject">Object to add</param>
        public void Add(GameObject gameObject)
        {
            gameObjects.Add(gameObject);

            // Keep track of most recent board added
            if (gameObject.GetType() == typeof(Board))
            {
                currentBoard = (Board)gameObject;
            }
        }

        /// <summary>
        /// Draw all objects in game object list
        /// </summary>
        public void Draw()
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Draw();
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
