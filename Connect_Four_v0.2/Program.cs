﻿using System;
using ConnectFourLibrary;
using Connect_Four_v0._2;

namespace App
{
    public class ConnectFour
    {
        static public void MainGameLoop(IUI consoleUI)
        {
            IBoard connectFourBoard = new Board(6, 7);
            IPlayer playerOne = new Player(1);
            IPlayer playerTwo = new Player(2);
            IGame gameManager = new Game(connectFourBoard, playerOne, playerTwo);
            int userInput;

            while (true)
            {
                consoleUI.DrawBoard(gameManager.Board);
                if ((userInput = consoleUI.UserInput()) == -1)
                    continue;
                if (gameManager.RegisterMoveToBoard(userInput - 1) == -1)
                {
                    consoleUI.HandleInvalidMove();
                    continue;
                }
                if (gameManager.CheckConnectedFour())
                    break;
                gameManager.SwapPlayerTurns();
                consoleUI.ClearConsole();
            }
            consoleUI.ClearConsole();
            consoleUI.DrawBoard(gameManager.Board);
            consoleUI.AnnounceWinner(gameManager);
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
                    consoleUI.AskForReplay(ref replay);
                }
            }
            catch (ArgumentException error)
            {
                Console.WriteLine(error.Message);
                return (1);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return (1);
            }
            return (0);
        }
    }
}
