using System;

namespace minesweeper
{
    struct GameMineSweeper
    {
        private BoardField board;
        private bool quitGame;

        //konstruktor här

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
        //UPPDATERAD
        private void TryUserCommand(string input)
        {
            char command = input[0];
            char blank = input[1];
            int col = ChangeLetterToNumber(input[2]);
            int row = (int)Char.GetNumericValue(input[3]);

            if (command.Equals('f') || command.Equals('r') || command.Equals('q'))
            {
                if(!blank.Equals(' ')|| row>9 || col>9)
                {
                    Console.WriteLine("Syntax error");
                }
                else
                {
                    if(command.Equals('f'))
                    {
                        board.FlagPostion(row, col);
                    }
                    else if(command.Equals('r'))
                    {
                        board.SweepPostion(row, col);
                    }
                    else
                    {
                        quitGame = true;
                    }
                }
            }
            else
            {
                Console.WriteLine("Unknown command");
            }
        }

        private void RunGame()
        {
            board.DrawField();
            string userInput = Console.ReadLine();
            TryUserCommand(userInput.ToLower());

            board.DrawField();

            Console.ReadKey();
           
            //while(quit != true || Gameover || Winner)
            // räkna flaggor??

        }
    }

}
