using System;
using VisualNovelEngine.EngineFiles;

namespace VisualNovelEngine
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class RunGame
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new EngineCore())
            {
                game.Run();
            }
        }
    }
}
