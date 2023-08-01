using ConnectFourLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_Four_v0._2
{
    public enum Line
    {
        TopOrBottom,
        Middle
    }

    public static class Constants
    {
        public static readonly string RED = "\u001b[31m";
        public static readonly string YELLOW = "\u001b[33m";
        public static readonly string COLOUR_RESET = "\u001b[0m";
        public static readonly string ROWLINE = "|-----|-----|-----|-----|-----|-----|-----|";
        public static readonly string PLAYER1_TOP_AND_BOTTOM = $"|{YELLOW} *** {COLOUR_RESET}";
        public static readonly string PLAYER1_MIDDLE = $"|{YELLOW}*   *{COLOUR_RESET}";
        public static readonly string PLAYER2_TOP_AND_BOTTOM = $"|{RED} *** {COLOUR_RESET}";
        public static readonly string PLAYER2_MIDDLE = $"|{RED}*   *{COLOUR_RESET}";
        public static readonly string EMPTY = "|     ";
        public static readonly string TOKEN_INPUT_INVALID = $"{RED}Nummer is niet tussen de 1 en de 7.{COLOUR_RESET}\n";
        public static readonly string NOT_A_NUMBER_INVALID = $"{RED}Voer enkel en alleen een nummer in tussen de 1 en de 7.{COLOUR_RESET}\n";
        public static readonly string COLUMN_IS_FULL = $"{RED}Kolom is vol, voer een kolom in die nog niet vol zit.{COLOUR_RESET}\n";
        public static readonly string ASK_INTEGER_INPUT = "\nVoer een nummer in tussen de 1 en de 7.";
        public static readonly string ASK_FOR_REPLAY = "\nDo you want to replay the game? (y/n)";
        public static readonly string REPLAY_CHARACTER_INVALID = $"{RED}Invalid input. Please press either 'y' or 'n'.{COLOUR_RESET}";
        public static readonly string ANNOUNCE_DRAW = "Truly equally skilled, this round knew no winner!";
    }

    public class ConsoleUI : IUI
    {
        private static string CreateDynamicRow(Line drawPosition, IBoard board, int row)
        {
            string drawString = "";

            for (int i = 0; i < board.PlayingBoard.GetLength(1); i++)
            {
                if (board.PlayingBoard[row, i].color == TokenColour.Yellow)
                {
                    if (drawPosition == Line.TopOrBottom)
                    {
                        drawString += Constants.PLAYER1_TOP_AND_BOTTOM;
                    }
                    else
                    {
                        drawString += Constants.PLAYER1_MIDDLE;
                    }
                }
                else if (board.PlayingBoard[row, i].color == TokenColour.Yellow)
                {
                    if (drawPosition == Line.TopOrBottom)
                    {
                        drawString += Constants.PLAYER2_TOP_AND_BOTTOM;
                    }
                    else
                    {
                        drawString += Constants.PLAYER2_MIDDLE;
                    }
                }
                else
                {
                    drawString += Constants.EMPTY;
                }
            }
            drawString += "|";
            return (drawString);
        }

        public void DrawBoard(IBoard board)
        {
            int linesToDraw = board.PlayingBoard.GetLength(0) * 4 + 1;

            for (int i = 0; i < linesToDraw; i++)
            {
                if (i % 4 == 0)
                {
                    Console.WriteLine(Constants.ROWLINE);
                }
                else if (i % 4 == 1 || i % 4 == 3)
                {
                    Console.WriteLine(CreateDynamicRow(Line.TopOrBottom, board, i / 4));
                }
                else
                {
                    Console.WriteLine(CreateDynamicRow(Line.Middle, board, i / 4));
                }
            }
        }

        private static void HandleValidationError(string errorMessage)
        {
            Console.Clear();
            Console.WriteLine(errorMessage);
        }

        private static void ValidateInput(int userInput)
        {
            if (userInput < 1 || userInput > 7)
            {
                throw new ArgumentException(Constants.TOKEN_INPUT_INVALID);
            }
        } 

        private static int ReadUserInput()
        {
            Console.WriteLine(Constants.ASK_INTEGER_INPUT);
            if (int.TryParse(Console.ReadLine(), out int userInput))
            {
                return userInput;
            }
            throw new ArgumentException(Constants.NOT_A_NUMBER_INVALID);
        }

        public int UserInput()
        {
            try
            {
                int userInput = ReadUserInput();
                ValidateInput(userInput);
                return userInput;
            }
            catch (ArgumentException error)
            {
                HandleValidationError(error.Message);
                return -1;
            }
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public void HandleFullColumn()
        {
            Console.Clear();
            Console.WriteLine(Constants.COLUMN_IS_FULL);
        }

        private char GetValidRematchInput()
        {
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                char userInput = cki.KeyChar;

                if (userInput == 'y' || userInput == 'n')
                {
                    return userInput;
                }
                Console.Clear();
                Console.WriteLine(Constants.REPLAY_CHARACTER_INVALID);
            }
        }

        private static string AnnounceWinner(string playerTurn)
        {
            string message = $"Congratulations player {playerTurn} on winning this round!";
            return message;
    }

        private static bool DetermineWinner(IGame gameManager)
        {
            return (gameManager.MaxPossibleMoves == 0 ? false : true);
        }

        public void AnnounceResult(IGame gameManager)
        {
            bool hasSomeoneWon = DetermineWinner(gameManager);

            if (hasSomeoneWon)
            {
                Console.WriteLine(AnnounceWinner(gameManager.PlayerTurn.PlayerColour.ToString()));
            }
            else
            {
                Console.WriteLine(Constants.ANNOUNCE_DRAW);
            }
        }

        public void Rematch(ref bool replay)
        {
            while (true)
            {
                Console.WriteLine(Constants.ASK_FOR_REPLAY);
                char userInput = GetValidRematchInput();

                if (userInput == 'y')
                {
                    return ;
                }
                else if (userInput == 'n')
                {
                    replay = false;
                    return ;
                }
            }
        }

        public void IntroductionMessage()
        {
            Console.WriteLine("Welcome to");
            Console.WriteLine(@$"{Constants.YELLOW}  ______ {Constants.RED}  ______  {Constants.YELLOW} .__   __.{Constants.RED} .__   __.{Constants.YELLOW}  _______ {Constants.RED}  ______ {Constants.YELLOW}.___________.{Constants.RED}    _  _    {Constants.COLOUR_RESET}");
            Console.WriteLine(@$"{Constants.YELLOW} /      |{Constants.RED} /  __  \ {Constants.YELLOW} |  \ |  |{Constants.RED} |  \ |  |{Constants.YELLOW} |   ____|{Constants.RED} /      |{Constants.YELLOW}|           |{Constants.RED}   | || |   {Constants.COLOUR_RESET}");
            Console.WriteLine(@$"{Constants.YELLOW}|  ,----'{Constants.RED}|  |  |  |{Constants.YELLOW} |   \|  |{Constants.RED} |   \|  |{Constants.YELLOW} |  |__   {Constants.RED}|  ,----'{Constants.YELLOW}`---|  |----`{Constants.RED}   | || |_  {Constants.COLOUR_RESET}");
            Console.WriteLine(@$"{Constants.YELLOW}|  |     {Constants.RED}|  |  |  |{Constants.YELLOW} |  . `  |{Constants.RED} |  . `  |{Constants.YELLOW} |   __|  {Constants.RED}|  |     {Constants.YELLOW}    |  |     {Constants.RED}   |__   _| {Constants.COLOUR_RESET}");
            Console.WriteLine(@$"{Constants.YELLOW}|  `----.{Constants.RED}|  `--'  |{Constants.YELLOW} |  |\   |{Constants.RED} |  |\   |{Constants.YELLOW} |  |____ {Constants.RED}|  `----.{Constants.YELLOW}    |  |     {Constants.RED}      | |   {Constants.COLOUR_RESET}");
            Console.WriteLine(@$"{Constants.YELLOW} \______|{Constants.RED} \______/ {Constants.YELLOW} |__| \__|{Constants.RED} |__| \__|{Constants.YELLOW} |_______|{Constants.RED} \______|{Constants.YELLOW}    |__|     {Constants.RED}      |_|   {Constants.COLOUR_RESET}");
            Console.WriteLine("\nPress any key to begin");
            Console.ReadKey();
            Console.Clear();
        }

        public void Rules()
        {
            Console.WriteLine("How to Play:");
            Console.WriteLine("1. Choose who plays first. Player 1 will be yellow and Player 2 will be red.");
            Console.WriteLine("2. Players will alternate taking turns in putting their tokens in the board. You will put your token in the board by typing in the number at the top of the column you wish to put it in.");
            Console.WriteLine("3. The first player to connect four tokens in a row wins. The four in a row can be horizontal, vertical or diagonal.");

            Console.WriteLine("\nPress a Key to begin.");
            Console.ReadKey();
        }
    }
}
