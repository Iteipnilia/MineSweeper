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

        public bool GameOver
        { 
            set { }
        }

        public void IncrementneighbouringMines()
        {

        }

        //UPPDATERAD
        public bool TryFlag()
        {
            if(flag==true)
            {
                Console.WriteLine("Removing flag from position");
                //flagcount--;
                symbol = (char)GameSymbols.SymbolDefault;
                return false;
            }
            else if(sweeped == true)
            {
                Console.WriteLine("Not allowed");
                return false;
            }
            else
            {
                // flagcount++;
                symbol = (char)GameSymbols.Flag;
                return true;
            }
        } 

        // UPPDATERAD
        public bool TrySweep()
        {
            if (!sweeped && !flag)
            {
                sweeped = true;
                if (neighbouringMines == 0)
                {
                    symbol = (char)GameSymbols.EmptyNoNearMines;
                }
                else
                {
                    symbol = char.Parse(neighbouringMines.ToString());
                }
                return true;
            }
            else
            {
                Console.WriteLine("Not allowed");
                return false;
            }
        }



    }
}
