namespace Labyrinth
{
    /// <summary>
    /// An open-source Labyrinth game using OpenTK
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            GameWindow window = new GameWindow(1280, 720);
            window.Run();
        }
    }
}
