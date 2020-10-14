using System;

namespace minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            GameMineSweeper game = new GameMineSweeper(args);

            game.RunGame();
        }
    }
}
