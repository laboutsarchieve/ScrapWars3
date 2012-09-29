using System;

namespace ScrapWars3
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using(ScrapWarsApp game = new ScrapWarsApp())
            {
                game.Run();
            }
        }
    }
#endif
}

