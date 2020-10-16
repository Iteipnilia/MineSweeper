using System;

namespace minesweeper
{
    struct BoardField
    {
        // FIELDS
        private BoardContent[,] boardfield;
        private int flagcount;
        private int sweepcount;
        private bool gameOver;
        private bool playerWon;

        //==============
        // CONSTRUCTOR
        //==============
        public BoardField(string[] args)
        {
            flagcount=0;
            sweepcount=0;
            gameOver = false;
            playerWon = false;

            boardfield = new BoardContent[10, 10];
            Helper.Initialize(args);

            //Fills the board with mines
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    bool boobyTrap = Helper.BoobyTrapped(row, col);
                    boardfield[row, col] = new BoardContent(boobyTrap);
                }
            }
            // Counting nearby mines for each position
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
                                if (boardfield[r, c].IsMine == true)
                                {
                                    boardfield[row, col].IncrementNeighbouringMines();
                                }
                            }
                        }
                    }
                }
            }
        }

        //====================================
        // PROPERTIES: GAME OVER & PLAYER WON
        //====================================
        public bool GameOver => gameOver;
        public bool PlayerWon => playerWon;

        //==========================
        // FLAG A POSITION
        //==========================
        public bool FlagPostion(int row, int col)
        {
            if (flagcount>=25 && !boardfield[row, col].IsFlag)
            {
                Console.WriteLine("Not allowed");
                return false;
            }
            else
            {
                return boardfield[row, col].TryFlag();
            }
        }

        //=======================
        // SWEEP A POSITION
        //=======================
        public bool SweepPostion(int row, int col)
        {
            if (boardfield[row, col].IsMine == true && boardfield[row,col].IsFlag !=true ) { gameOver = true; }

            if (boardfield[row, col].NeighbouringMines == 0 && !boardfield[row, col].IsSweeped)
            {
                SweepNearby(row, col);
            }

            else { return boardfield[row, col].TrySweep(); }
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
                        if (boardfield[r, c].IsSweeped != true && boardfield[r,c].IsFlag !=true)
                        { 
                            boardfield[r, c].TrySweep();
                            
                            if (boardfield[r, c].NeighbouringMines == 0) { SweepNearby(r, c); }
                        }
                    }
                }
            }
        }

        //===================================
        // DRAWFIELD: Prints out a 2D array
        //===================================
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
                    if(gameOver== true) {Console.Write(" " + boardfield[row, col].GameOver() + ""); }
                    else
                    {
                        Console.Write(" " + boardfield[row, col].Symbol + "");

                        // Counting flags and sweeped positions
                        if (boardfield[row, col].IsFlag == true) { countflag++; }
                        if (boardfield[row, col].IsSweeped == true) { countsweep++; }
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