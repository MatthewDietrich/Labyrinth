namespace Labyrinth
{
    /// <summary>
    /// An object in the game space. Serves as a base class for more specific classes.
    /// </summary>
    class GameObject
    {
        public int VertexOffset { get; set; }
        public int IndexOffset { get; set; }

        public ColoredVertex[] Vertices { get; protected set; }
        public uint[] Indices { get; protected set; }

        public GameObject()
        {

        }
    }
}
