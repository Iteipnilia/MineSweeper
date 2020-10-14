using System;

namespace minesweeper
{
    struct BoardField
    {
        private BoardContent[,] fieldBoard;
        private int flagcount;
        private int sweepCount;
        private bool gameOver;
        private bool playerWon;

        //konstruktor
        public BoardField(string[] args)
        {
            flagcount=0;
            sweepCount=0;
            gameOver = false;
            playerWon = false;

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
            // räknar närliggande minor
            for (int row = 0; row < 10; ++row)
            {
                for (int col = 0; col < 10; ++col)
                {
                    for (int r = row - 1; r <= row + 1; r++)
                    {
                        for (int c = col - 1; c <= col + 1; c++)
                        {
                            if (r >= 0 && r < 10 && c >= 0 && c < 10)
                            {
                                if (fieldBoard[r, c].IsMine == true)
                                {
                                    fieldBoard[row, col].IncrementNeighbouringMines();
                                }
                            }
                        }
                    }
                }
            }

        }

        // Enbart läsbar egenskap som säger som spelaren har vunnit spelet.
        public bool PlayerWon =>playerWon; // uppdaterad

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

        //RÖJ en position
        public bool SweepPostion(int row, int col)
        {

            if (fieldBoard[row, col].IsMine == true) { gameOver = true; }

            if (fieldBoard[row, col].NeighbouringMines == 0)
            {
                SweepNearby(row, col);
            }

            else { fieldBoard[row, col].TrySweep(); }
            return true;          
        }

        public void SweepNearby(int row, int col)
        {
            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    if (r >= 0 && r < 10 && c >= 0 && c < 10)
                    {
                        if (fieldBoard[r, c].IsSweeped != true)
                        { 
                            fieldBoard[r, c].TrySweep();
                            
                            if (fieldBoard[r, c].NeighbouringMines == 0) { SweepNearby(r, c); }
                        }

                    }
                }
            }
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
                    if(gameOver== true) {Console.Write(" " + fieldBoard[row, col].GameOver() + " "); }

                    else
                    {
                        Console.Write(" " + fieldBoard[row, col].Symbol + " ");

                        //UPPDATERAT RÄKNAR FLAGGOR OCH RÖJNING
                        if (fieldBoard[row, col].IsFlag == true) { countflag++; }
                        if (fieldBoard[row, col].IsSweeped == true) { countsweep++; }
                    }

                }
                Console.WriteLine();
            }
            flagcount=countflag;
            sweepCount=countsweep;
            if (sweepCount == 90) { playerWon = true; }// finns bättre sätt??
            Console.Write("\n >");
        }


    }
}
