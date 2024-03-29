﻿using System;

namespace minesweeper
{
    //==========================================
    // Constant types for Gameplay and Gameover
    //==========================================
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
        // FIELDS
        private int neighbouringMines;
        private bool mine;
        private bool flag;
        private bool sweeped;
        private char symbol;

        //==============
        // CONSTRUCTOR
        //==============
        public BoardContent(bool boobytrap)
        {
            neighbouringMines = 0;
            mine = boobytrap;
            flag = false;
            sweeped = false;
            symbol = (char)GameSymbols.SymbolDefault;
        }

        //============
        // PROPERTIES
        //============
        public char Symbol => symbol;
        public bool IsSweeped => sweeped;
        public bool IsMine => mine;
        public bool IsFlag => flag;
        public int NeighbouringMines
        {
            get {return neighbouringMines;}
            set { neighbouringMines= value;}
        }

        //=============================
        // CALCULATOR FOR NEARBY MINES
        //=============================
        public void IncrementNeighbouringMines()
        {
            neighbouringMines += 1;
        }

        //==============================
        // CHANGES SYMBOLS IF GAME OVER
        //==============================
        public char GameOver()
        {
            if (flag && mine) { return symbol = (char)GameOverSymbols.MineFlagged; }

            if (sweeped && mine) { return symbol = (char)GameOverSymbols.ExplodedMine; }

            if (mine && !sweeped) { return symbol = (char)GameOverSymbols.Mine; }

            if (flag && !mine) { return symbol = (char)GameOverSymbols.FlaggedWrong; }

            if (!(flag || mine) && neighbouringMines >= 1) { return symbol = char.Parse(neighbouringMines.ToString());}

            return symbol = (char)GameSymbols.EmptyNoNearMines;
        }

        //======================================
        // TRYFLAG: Adds or removes flag from
        // postion depending on conditions
        //======================================
        public bool TryFlag()
        {
            if(flag==true)
            {
                Console.WriteLine("\nRemoving flag from position");
                symbol = (char)GameSymbols.SymbolDefault;
                flag=false;
                return true;
            }
            else if(sweeped == true)
            {
                Console.WriteLine("Not allowed");
                return false;
            }
            else
            {
                symbol = (char)GameSymbols.Flag; 
                flag=true;
                return true;
            }
        } 

        //=========================================
        // TRYSWEEP: checks if sweeping is allowed
        // Sweepes position if true
        //=========================================
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
                sweeped =true;
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