// Based on https://github.com/amulware/genericgamedev-opentk-intro

using OpenTK;
using OpenTK.Graphics;

namespace Labyrinth
{
    /// <summary>
    /// A vertex with position and color information associated with it
    /// </summary>
    struct ColoredVertex
    {
        public const int Size = (3 + 4) * 4;

        private readonly Vector3 position;
        private readonly Color4 color;

        /// <summary>
        /// A vertex with position and color information associated with it
        /// </summary>
        /// <param name="position">Position in object space</param>
        /// <param name="color">Color of vertex</param>
        public ColoredVertex(Vector3 position, Color4 color)
        {
            this.position = position;
            this.color = color;
        }
    }
}
