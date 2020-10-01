using System;

namespace minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            //För att testa utskrift av minfältet
            BoardField board = new BoardField(args);

            board.DrawField();
        }
    }
}
