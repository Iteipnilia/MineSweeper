using System;

namespace minesweeper
{
    struct BoardField
    {
        private BoardContent[,] fieldBoard;
        private int flagcount;

        //konstruktor
        public BoardField(string[] args)   //(BoardContent initField)
        {
            flagcount=0;

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

        //Flagga en position (UPPDATERAD)
        public bool FlagPostion(int row, int col)
        {
            if (flagcount==25)
            {
                Console.WriteLine("Not allowed");
                return false;
            }
            else
            {
                return fieldBoard[row, col].TryFlag();
            }
        }

        //RÖJ en position (UPPDATERAD)
        public bool SweepPostion(int row, int col)
        {
            return fieldBoard[row, col].TrySweep();
        }

        // utskrift av 2D array
        // UPPDATERAD (DESIGN)
        public void DrawField()
        {
            Console.WriteLine("     A  B  C  D  E  F  G  H  I  J ");
            Console.WriteLine("   +------------------------------");
            for (int row = 0; row < 10; row++)
            {
                Console.Write($" {row} |");
                for (int col = 0; col < 10; col++)
                {
                    Console.Write(" " + fieldBoard[row,col].Symbol + " "); // skriver ut symbol för default
                }
                Console.WriteLine();
            }
            Console.Write("\n >");
        }


    }
}
