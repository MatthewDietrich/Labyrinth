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
        public const int Size = (3 + 4) * 4; // (3 floats in Vector3 + 4 floats in Color 4) * 4 bytes per float

        public Vector3 Position { get; }
        public Color4 Color { get; }

        /// <summary>
        /// A vertex with position and color information associated with it
        /// </summary>
        /// <param name="position">Position in object space</param>
        /// <param name="color">Color of vertex</param>
        public ColoredVertex(Vector3 position, Color4 color)
        {
            Position = position;
            Color = color;
        }
    }
}
