using System;

namespace minesweeper
{
    struct GameMineSweeper
    {
        private BoardField board;
        private bool quitGame;

        //konstruktor
        public GameMineSweeper(string[] args)
        {
            board = new BoardField(args);
            quitGame = false;
        }

        //Changes a letter to a number
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
        //UPPDATERAD IGEN
        private void UserCommand(string input)
        {
            if (input.Length.Equals(1))
            {
                char command = input[0];
                if(command.Equals('q'))
                {
                    quitGame = true;
                }
                else { Console.WriteLine("Unknown command"); }
            }
            else if (input.Length.Equals(2)|| input.Length.Equals(3)) { Console.WriteLine("Syntax error"); }

            else if (input.Length.Equals(4))
            {
                char command = input[0];
                char blank = input[1];
                int col = ChangeLetterToNumber(input[2]);
                int row = (int)Char.GetNumericValue(input[3]);

                if (command.Equals('f') || command.Equals('r'))
                {
                    if (!blank.Equals(' '))
                    {
                        Console.WriteLine("Syntax error");
                    }
                    else
                    {
                        if (command.Equals('f'))
                        {
                            board.FlagPostion(row, col);
                        }
                        else if (command.Equals('r'))
                        {
                            board.SweepPostion(row, col);
                        }
                    }
                }
                else { Console.WriteLine("Unknown command"); }
            }
            else { Console.WriteLine("Syntax error"); }
        }

        public void RunGame()
        {
            do
            {
                board.DrawField();
                string userInput = Console.ReadLine();
                UserCommand(userInput.ToLower());

            } while(quitGame !=true);
           
            //while(quit != true || Gameover || Winner)
            // räkna flaggor??
        }
    }

}
