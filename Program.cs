using System;

namespace minesweeper
{
    class Program
    {

        static void Main(string[] args)
        {
            //För att testa utskrift av minfältet
            //UPPDATERAT
            GameMineSweeper game = new GameMineSweeper(args);

            game.RunGame();
        }
    }
}
