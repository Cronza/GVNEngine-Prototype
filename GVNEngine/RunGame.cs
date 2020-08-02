using System;
using GVNEngine.EngineFiles;

namespace GVNEngine
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
            using (var game = new Engine_Core())
            {
                //Begin Engine Execution / Run the Game
                game.Run();

                
                
            }
        }
    }
}
