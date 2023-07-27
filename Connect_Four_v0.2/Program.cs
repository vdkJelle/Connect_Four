using System;
using ConnectFourLibrary;
using Connect_Four_v0._2;

namespace App
{
    public class ConnectFour
    {
        static public void MainGameLoop(IUI consoleUI)
        {
            IBoard connectFourBoard = new Board(6, 7);
            IPlayer playerOne = new Player("One");
            IPlayer playerTwo = new Player("Two");
            IGame gameManager = new Game(connectFourBoard, playerOne, playerTwo);
            int userInput;

            for (; gameManager.MaxPossibleMoves > 0; gameManager.MaxPossibleMoves--)
            {
                consoleUI.DrawBoard(gameManager.Board);
                if ((userInput = consoleUI.UserInput()) == -1)
                {
                    continue;
                }
                if (gameManager.RegisterMoveToBoard(userInput - 1) == -1)
                {
                    consoleUI.HandleFullColumn();
                    continue;
                }
                if (gameManager.CheckForWinner())
                {
                    break;
                }
                gameManager.SwapPlayerTurns();
                consoleUI.ClearConsole();
            }
            consoleUI.ClearConsole();
            consoleUI.DrawBoard(gameManager.Board);
            consoleUI.AnnounceResult(gameManager);
        }

        static public int Main()
        {
            try
            {
                IUI consoleUI = new ConsoleUI();
                consoleUI.IntroductionMessage();
                consoleUI.Rules();

                bool replay = true;

                while (replay)
                {
                    MainGameLoop(consoleUI);
                    consoleUI.Rematch(ref replay);
                }
            }
            catch (ArgumentException error)
            {
                Console.WriteLine(error.Message);
                return 1;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return 1;
            }
            return 0;
        }
    }
}
