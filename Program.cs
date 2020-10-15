using System;
using System.Text;

namespace minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            GameMineSweeper game = new GameMineSweeper(args);

            game.RunGame();
        }
    }
}
