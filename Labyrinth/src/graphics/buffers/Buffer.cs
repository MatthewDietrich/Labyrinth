using System;
using OpenTK.Graphics.OpenGL;

namespace Labyrinth
{
    /// <summary>
    /// Class that contains an array of elements, and methods to pass the array (or subsets of the array) to the GL buffer as needed
    /// </summary>
    /// <typeparam name="T">Type of buffer (will generally be either an unsigned int or a structure representing a vertex)</typeparam>
    internal class Buffer<T>
    {
        protected readonly int handle; // ID of GL Buffer
        protected int count; // Number of items currently in array (often less than the size of the array)
        protected T[] bufferItems;

        public Buffer()
        {
            handle = GL.GenBuffer();
            count = 0;
            bufferItems = new T[2];
        }

        /// <summary>
        /// Add a single item to the array, allocating more space if necessary.
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(T item)
        {
            if (count == bufferItems.Length)
                Array.Resize(ref bufferItems, count * 2);
            bufferItems[count] = item;
            count++;
        }

        /// <summary>
        /// Add an array of items to the array, allocating more space if necessary
        /// </summary>
        /// <param name="items"></param>
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
        /// Return a subset of elements from bufferItems
        /// </summary>
        /// <param name="begin">Index to begin at</param>
        /// <param name="end">Index to end at</param>
        /// <returns>Array of size (end - begin) containing all elements from bufferItems[begin] to bufferItems[end]</returns>
        protected T[] GetSubArray(int begin, int end)
        {
            T[] subArray = new T[end - begin];

            for (int i = begin; i < end; i++)
                subArray[i - begin] = bufferItems[i];

            return subArray;
        }

        /// <summary>
        /// Bind buffer to GL context
        /// </summary>
        public virtual void Bind() =>
            throw new NotImplementedException("Bind() must be called on a subclass of Buffer.");

        /// <summary>
        /// Put data in buffer
        /// </summary>
        public virtual void BufferData() =>
            throw new NotImplementedException("BufferData must be called on a subclass of Buffer.");

        /// <summary>
        /// Draw buffered data
        /// </summary>
        public virtual void Draw() =>
            throw new NotImplementedException("Draw() must be called on a subclass of Buffer.");

        /// <summary>
        /// Put subset of data in buffer
        /// </summary>
        /// <param name="offset">Offset of beginning of data</param>
        /// <param name="size">Number of data points</param>
        public virtual void BufferSubData(int offset, int size) =>
            throw new NotImplementedException("BufferSubData must be called on a subclass of Buffer");

        public virtual void Prepare(int size) => throw new NotImplementedException("Prepare must be called on subclass of Buffer");
    }
}
