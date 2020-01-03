/// @file
/// @brief Runs Zetris
/// 
/// @author Rodrigo Pinheiro e Tomás Franco
/// @date 2020

namespace Zetris
{
    class Program
    {
        /// <summary>
        /// Creates an instance of Zetris and runs it
        /// </summary>
        /// <param name="args"> Console arguments</param>
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
