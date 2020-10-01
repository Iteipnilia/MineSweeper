using System;
using System.Resources;

namespace Minesweeper
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



    }
}
