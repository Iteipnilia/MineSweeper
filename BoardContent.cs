using System;
using System.Resources;

namespace minesweeper
{

    //Enum ska vara utanför struct???
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

        // Om en mina finns i närheten ska det skrivas ut i antalet som finns, 1-8
        private int neighbouringMines;
        private bool mine;
        private bool flag;
        private bool sweeped;
        private char symbol;

        public BoardContent(bool boobytrap)
        {
            neighbouringMines = 0;
            mine = false;
            flag = false;
            sweeped = false;
            symbol = (char)GameSymbols.SymbolDefault;
        }
        
        public char Symbol => symbol;
        public bool IsSweeped => sweeped;
        public bool IsMine => mine;
        public bool IsFlag => flag;

        private int NeighbouringMines
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
        public bool GameOver
        {
            set 
            { 
                if (value)
                {
                    if (flag && mine){symbol = (char)GameOverSymbols.MineFlagged;}

                    if (sweeped && mine){symbol = (char)GameOverSymbols.ExplodedMine;}

                    if (mine && !sweeped){symbol = (char)GameOverSymbols.Mine;}

                    if (flag && !mine) {symbol = (char)GameOverSymbols.FlaggedWrong;}
                }
            } 
        }



        //UPPDATERAD 10/10
        public bool TryFlag()
        {
            if(flag==true)
            {
                Console.WriteLine("Removing flag from position");
                //flagcount--;
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
                // flagcount++;
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
