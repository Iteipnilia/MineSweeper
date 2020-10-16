using System;

namespace minesweeper
{
    struct GameMineSweeper
    {
        // FIELDS
        private BoardField board;
        private bool quitGame;
        private string userInput;
        private bool iscommandtrue;

        //=================
        // CONSTRUCTOR
        //=================
        public GameMineSweeper(string[] args)
        {
            iscommandtrue=false;
            userInput="";
            board = new BoardField(args);
            quitGame = false;
            
        }

        //==================================
        // Changes a char letter to a number
        //==================================
        private int ChangeLetterToNumber(char input)
        {
            // A-J == 0-9, a-j == 0-9
            if (input >= 'A' && input <= 'J')
            {
                input -= (char)('A' - 0);
            }

            else if(input >= 'a' && input <= 'j')
            {
                input -= (char)('a' - 0);
            }
            return (int)input;
        }

        //================================
        // USERCOMMAND: 
        // Tests if usercommand is correct.
        // Returns a value and passes 
        // command on to correct method
        //================================
        private bool UserCommand(string input)
        {
            if (Console.IsInputRedirected)
            {
                Console.WriteLine(input);
            }
            if (input.Length.Equals(1))
            {
                char command = input[0];

                if(command.Equals('q')) { quitGame=true; return true;}
                if (command >= 'a' && command <= 'ö') { Console.WriteLine("Unknown command"); return false; }
                else { Console.WriteLine("Syntax error"); return false; }
            }
            else if (input.Length.Equals(2)|| input.Length.Equals(3)) { Console.WriteLine("Syntax error"); return false; }

            else if (input.Length.Equals(4))
            {
                char command = input[0];
                char blank = input[1];
                int col = ChangeLetterToNumber(input[2]);
                int row = (int)Char.GetNumericValue(input[3]);

                {
                    if (!blank.Equals(' ')|| row > 9|| row < 0 || col > 9){Console.WriteLine("Syntax error"); return false;}

                    else
                    {
                        if (command.Equals('f')) {return board.FlagPostion(row, col);}

                        else if (command.Equals('r')){return board.SweepPostion(row, col);}

                        else { Console.WriteLine("Unknown command"); return false; }
                    }
                }
            }
            else { Console.WriteLine("Syntax error"); return false;}
        }

        //==============
        // RUN GAME
        //==============
        public void RunGame()
        {
            do
            {           
                    
                board.DrawField();
                do
                {
                    Console.Write("\n >");
                    userInput = Console.ReadLine();
                    iscommandtrue=UserCommand(userInput.ToLower());
                }
                while(iscommandtrue ==false);

                if (board.PlayerWon)
                {
                    board.DrawField();
                    Console.WriteLine("WELL DONE!");
                    Console.Read();
                    System.Environment.Exit(0);
                }

                if (board.GameOver)
                {
                    board.DrawField();
                    Console.WriteLine("GAME OVER");
                    Console.Read();
                    System.Environment.Exit(1);
                }
                if (quitGame)

                {
                    Console.Read();
                    System.Environment.Exit(2);
                }

            } 
            while (!(quitGame || board.PlayerWon || board.GameOver));
        }
    }

}
