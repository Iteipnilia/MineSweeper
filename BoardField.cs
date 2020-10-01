using System;

namespace Minesweeper
{
    struct BoardField
    {
        private BoardContent[,] fieldBoard;

        //konstruktor
        public BoardField(string[] args)   //(BoardContent initField)
        {
            fieldBoard = new BoardContent[10, 10];
            Helper.Initialize(args);

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    bool boobyTrap = Helper.BoobyTrapped(row, col);
                    fieldBoard[row, col] = new BoardContent(boobyTrap);
                }
            }
        }


        // utskrift av 2D array
        public void DrawField()
        {
            Console.WriteLine("   A  B  C  D  E  F  G  H  I  J ");
            Console.WriteLine(" +------------------------------");
            for (int row = 0; row < 10; row++)
            {
                Console.Write($"{row}|");
                for (int col = 0; col < 10; col++)
                {
                    Console.Write(" " + fieldBoard[row,col].Symbol + " "); // skriver ut symbol för default
                }
                Console.WriteLine();
            }
        }

    }
}
