using System;

namespace minesweeper
{
    struct BoardField
    {
        private BoardContent[,] fieldBoard;
        private int flagcount;
        private int sweepcount;
        private bool gameOver;
        private bool playerWon;

        //konstruktor
        public BoardField(string[] args)
        {
            flagcount=0;
            sweepcount=0;
            gameOver = false;
            playerWon = false;

            fieldBoard = new BoardContent[10, 10];
            Helper.Initialize(args);

            //Fills the board with mines
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    bool boobyTrap = Helper.BoobyTrapped(row, col);
                    fieldBoard[row, col] = new BoardContent(boobyTrap);
                }
            }
            // Counts nearby mines for each position
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

        //==================================
        // PROPERTYS: GAME OVER & PLAYER WON
        //==================================
        public bool PlayerWon =>playerWon; // uppdaterad
        public bool GameOver => gameOver;

        //==========================
        // FLAG A POSITION
        //==========================
        public bool FlagPostion(int row, int col)
        {
            if (flagcount>=25 && !fieldBoard[row, col].IsFlag)
            {
                Console.WriteLine("Not allowed");
                return false;
            }
            else
            {
                return fieldBoard[row, col].TryFlag();
            }
        }

        //=======================
        //SWEEP A POSITION
        //=======================
        public bool SweepPostion(int row, int col)
        {

            if (fieldBoard[row, col].IsMine == true && fieldBoard[row,col].IsFlag !=true ) { gameOver = true; }

            if (fieldBoard[row, col].NeighbouringMines == 0 && !fieldBoard[row, col].IsSweeped)//FIXAT NOT ALLOWED?
            {
                SweepNearby(row, col);
            }

            else { return fieldBoard[row, col].TrySweep(); }
            return true;          
        }

        //====================================
        // METHOD FOR SWEEPING NEARBY POSTIONS
        // (if there are no nearby mines)
        //====================================
        public void SweepNearby(int row, int col)
        {
            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    if (r >= 0 && r < 10 && c >= 0 && c < 10)
                    {
                        if (fieldBoard[r, c].IsSweeped != true && fieldBoard[r,c].IsFlag !=true)///FUNKA
                        { 
                            fieldBoard[r, c].TrySweep();
                            
                            if (fieldBoard[r, c].NeighbouringMines == 0) { SweepNearby(r, c); }
                        }

                    }
                }
            }
        }

        //=================================
        // DRAWFIELD: Prints out a 2D array
        //=================================
        public void DrawField()
        {
            int countflag=0;
            int countsweep=0;

            Console.WriteLine();
            Console.WriteLine("     A B C D E F G H I J");
            Console.WriteLine("   +--------------------");
            for (int row = 0; row < 10; row++)
            {
                Console.Write($" {row} |");
                for (int col = 0; col < 10; col++)
                {
                    if(gameOver== true) {Console.Write(" " + fieldBoard[row, col].GameOver() + ""); }
                    else
                    {
                        Console.Write(" " + fieldBoard[row, col].Symbol + "");

                        //UPPDATERAT RÄKNAR FLAGGOR OCH RÖJNING
                        if (fieldBoard[row, col].IsFlag == true) { countflag++; }
                        if (fieldBoard[row, col].IsSweeped == true) { countsweep++; }
                    }

                }
                Console.WriteLine();
            }
            flagcount=countflag;
            sweepcount=countsweep;
            if(sweepcount==89){playerWon=true;}
        }


    }
}