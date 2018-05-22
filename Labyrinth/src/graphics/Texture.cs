using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Labyrinth
{
    class Texture
    {
        private int id;
        private int width, height;
        private string path;

        public int ID { get { return id; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }

        /// <summary>
        /// Used to keep track of file names, texture IDs, and dimensions
        /// </summary>
        /// <param name="path">Location of image file to load as texture</param>
        public Texture(string path)
        {
            if (!File.Exists("textures/" + path))
            {
                throw new FileNotFoundException("File not found at Content/" + path);
            }

            id = GL.GenTexture(); // Create new Texture ID
            GL.BindTexture(TextureTarget.Texture2D, id); // Bind new texture ID

            Bitmap bmp = new Bitmap("textures/" + path); // Create new bitmap of image

            // Store imensions
            width = bmp.Width;
            height = bmp.Height;

            // Retrieve bitmap data
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Add texture to OpenGL
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
    }
}