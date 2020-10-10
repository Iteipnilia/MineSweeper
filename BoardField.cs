using System;

namespace minesweeper
{
    struct BoardField
    {
        private BoardContent[,] fieldBoard;
        private int flagcount;
        private int sweepCount;
        // Läggas till??
        private bool playerWon;
        private bool gameOver;

        //konstruktor
        public BoardField(string[] args)   //(BoardContent initField)
        {
            flagcount=0;
            sweepCount=0;
            playerWon=false;
            gameOver=false;

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

        // Enbart läsbar egenskap som säger som spelaren har vunnit spelet.
        public bool PlayerWon => playerWon; // uppdaterad

        // Enbart läsbar egenskap som säger om spelaren har förlorat.
        public bool GameOver => gameOver; // uppdaterad

        //Flagga en position (UPPDATERAD)
        public bool FlagPostion(int row, int col)
        {
            if (flagcount>=25)// ska  vara större eller lika med 25
            {
                Console.WriteLine("Not allowed");
                return false;
            }
            else
            {
                return fieldBoard[row, col].TryFlag();
            }
        }

        //RÖJ en position (UPPDATERAD) game over
        public bool SweepPostion(int row, int col)
        {
            if(fieldBoard[row,col].IsMine==true)
            {
                return gameOver=true;
            }
            else {return fieldBoard[row, col].TrySweep();}
        }

        // utskrift av 2D array
        // UPPDATERAD (DESIGN)
        public void DrawField()
        {
            int countflag=0;
            int countsweep=0;
            Console.WriteLine("     A  B  C  D  E  F  G  H  I  J ");
            Console.WriteLine("   +------------------------------");
            for (int row = 0; row < 10; row++)
            {
                Console.Write($" {row} |");
                for (int col = 0; col < 10; col++)
                {
                    Console.Write(" " + fieldBoard[row,col].Symbol + " "); // skriver ut symbol för default
                    //UPPDATERAT RÄKNAR FLAGGOR OCH RÖJNING
                    if(fieldBoard[row,col].IsFlag==true){countflag++;}
                    if(fieldBoard[row,col].IsSweeped==true){countsweep++;}
                }
                Console.WriteLine();
            }
            flagcount=countflag;
            sweepCount=countsweep;
            Console.Write("\n >");
            Console.WriteLine($"\nFlaggor: {flagcount} och Röjda: {sweepCount}");
        }


    }
}
