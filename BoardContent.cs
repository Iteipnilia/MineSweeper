using System;

namespace minesweeper
{
    enum GameSymbols
    {
        SymbolDefault = 'X',
        Flag = 'F',
        EmptyNoNearMines = '.'
    }

    enum GameOverSymbols
    {
        ExplodedMine = 'M',
        Mine = 'm',
        MineFlagged = 'w',
        FlaggedWrong = 'Ⅎ' 
    }
    struct BoardContent
    {
        private int neighbouringMines;
        private bool mine;
        private bool flag;
        private bool sweeped;
        private char symbol;
        public BoardContent(bool boobytrap)
        {
            neighbouringMines = 0;
            mine = boobytrap;
            flag = false;
            sweeped = false;
            symbol = (char)GameSymbols.SymbolDefault;
        }
        
        public char Symbol => symbol;
        public bool IsSweeped => sweeped;
        public bool IsMine => mine;
        public bool IsFlag => flag;

        public int NeighbouringMines
        {
            get {return neighbouringMines;}
            set { neighbouringMines= value;}
        }

        // Öka räknaren av minor på intilliggande rutor med 1.
        public void IncrementNeighbouringMines() // Stubbe
        {
            neighbouringMines += 1;
        }

        //SKRIVS UT VID GAME OVER
        public char GameOver()
        {
            if (flag && mine) { return symbol = (char)GameOverSymbols.MineFlagged; }

            if (sweeped && mine) { return symbol = (char)GameOverSymbols.ExplodedMine; }

            if (mine && !sweeped) { return symbol = (char)GameOverSymbols.Mine; }

            if (flag && !mine) { return symbol = (char)GameOverSymbols.FlaggedWrong; }

            return symbol = (char)GameSymbols.EmptyNoNearMines;
        }



        //UPPDATERAD 10/10
        public bool TryFlag()
        {
            if(flag==true)
            {
                Console.WriteLine("Removing flag from position");
                symbol = (char)GameSymbols.SymbolDefault;
                return  flag=false;
            }
            else if(sweeped == true)
            {
                Console.WriteLine("Not allowed");
                return false;//U
            }
            else
            {
                symbol = (char)GameSymbols.Flag;
                return flag=true;
            }
        } 

        // UPPDATERAD
        public bool TrySweep()
        {
            if (!sweeped && !flag)
            {
                if (neighbouringMines == 0)
                {
                     symbol = (char)GameSymbols.EmptyNoNearMines; 
                }
                else
                {
                    symbol = char.Parse(neighbouringMines.ToString());
                }
                return sweeped =true;
            }
            else
            {
                Console.WriteLine("Not allowed");
                return false;
            }
        }



    }
}
