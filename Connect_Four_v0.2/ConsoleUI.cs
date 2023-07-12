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
        public static readonly string ROWLINE = "|-----|-----|-----|-----|-----|-----|-----|";
        public static readonly string PLAYER1_TOP_AND_BOTTOM = "|\u001b[33m *** \u001b[0m";
        public static readonly string PLAYER1_MIDDLE = "|\u001b[33m*   *\u001b[0m";
        public static readonly string PLAYER2_TOP_AND_BOTTOM = "|\u001b[31m *** \u001b[0m";
        public static readonly string PLAYER2_MIDDLE = "|\u001b[31m*   *\u001b[0m";
        public static readonly string EMPTY = "|     ";
    }

    public class ConsoleUI : IUI
    {
        private static string CreateDynamicRow(Line drawPosition, IBoard board, int row)
        {
            string drawString = "";

            for (int i = 0; i < board.PlayingBoard.GetLength(1); i++)
            {
                if (board.PlayingBoard[row, i] == 1)
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
                else if (board.PlayingBoard[row, i] == 2)
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
                throw new ArgumentException("\u001b[31mNummer is niet tussen de 1 en de 7.\u001b[0m\n");
            }
        } 

        private static int ReadUserInput()
        {
            Console.WriteLine("\nVoer een nummer in tussen de 1 en de 7.");
            if (int.TryParse(Console.ReadLine(), out int userInput))
            {
                return userInput;
            }
            throw new ArgumentException("\u001b[31mVoer enkel en alleen een nummer in tussen de 1 en de 7.\u001b[0m\n");
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

        public void HandleInvalidMove()
        {
            Console.Clear();
            Console.WriteLine("\u001b[31mKolom is vol, voer een kolom in die nog niet vol zit.\u001b[0m\n");
        }

        private char GetValidReplayInput()
        {
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                char userInput = cki.KeyChar;

                if (userInput == 'y' || userInput == 'n')
                {
                    return userInput;
                }
            }
        }

        public void AnnounceWinner(IGame gameManager)
        {
            Console.WriteLine($"Congratulations player {gameManager.PlayerTurn} on winning this round!");
        }

        public void AskForReplay(ref bool replay)
        {
            while (true)
            {
                Console.WriteLine("\nDo you want to replay the game? (y/n)");
                char userInput = GetValidReplayInput();

                if (userInput == 'y')
                {
                    return ;
                }
                else if (userInput == 'n')
                {
                    replay = false;
                    return ;
                }

                Console.Clear();
                Console.WriteLine("\n\u001b[31mInvalid input. Please press either 'y' or 'n'.\u001b[0m");
            }
        }

        public void IntroductionMessage()
        {
            Console.WriteLine("Welcome to");
            Console.WriteLine(@"[33m  ______ [31m  ______  [33m .__   __.[31m .__   __.[33m  _______ [31m  ______ [33m.___________.[31m    _  _    [0m");
            Console.WriteLine(@"[33m /      |[31m /  __  \ [33m |  \ |  |[31m |  \ |  |[33m |   ____|[31m /      |[33m|           |[31m   | || |   [0m");
            Console.WriteLine(@"[33m|  ,----'[31m|  |  |  |[33m |   \|  |[31m |   \|  |[33m |  |__   [31m|  ,----'[33m`---|  |----`[31m   | || |_  [0m");
            Console.WriteLine(@"[33m|  |     [31m|  |  |  |[33m |  . `  |[31m |  . `  |[33m |   __|  [31m|  |     [33m    |  |     [31m   |__   _| [0m");
            Console.WriteLine(@"[33m|  `----.[31m|  `--'  |[33m |  |\   |[31m |  |\   |[33m |  |____ [31m|  `----.[33m    |  |     [31m      | |   [0m");
            Console.WriteLine(@"[33m \______|[31m \______/ [33m |__| \__|[31m |__| \__|[33m |_______|[31m \______|[33m    |__|     [31m      |_|   [0m");
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
