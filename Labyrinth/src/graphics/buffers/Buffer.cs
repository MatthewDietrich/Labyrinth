using System;
using OpenTK.Graphics.OpenGL;

namespace Labyrinth
{
    class Buffer<T>
    {
        protected readonly int handle;
        protected int count;
        protected T[] bufferItems;

        public Buffer()
        {
            handle = GL.GenBuffer();
            count = 0;
            bufferItems = new T[2];
        }

        public void Add(T item)
        {
            if (count == bufferItems.Length)
                Array.Resize(ref bufferItems, count * 2);
            bufferItems[count] = item;
            count++;
        }

        public void Add(T[] items)
        {
            foreach (T item in items)
            {
                if (count == bufferItems.Length)
                    Array.Resize(ref bufferItems, count * 2);
                bufferItems[count] = item;
                count++;
            }
        }

        /// <summary>
        /// Bind buffer to GL context
        /// </summary>
        public virtual void Bind() => throw new NotImplementedException("Bind() must be called on a subclass of Buffer.");

        /// <summary>
        /// Put data in buffer
        /// </summary>
        public virtual void BufferData() => throw new NotImplementedException("BufferData must be called on a subclass of Buffer.");

        /// <summary>
        /// Draw buffered data
        /// </summary>
        public virtual void Draw() => throw new NotImplementedException("Draw() must be called on a subclass of Buffer.");
    }
}
