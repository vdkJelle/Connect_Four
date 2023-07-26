using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourLibrary
{
    public class Game : IGame
    {
        public Game(IBoard playingBoard, IPlayer playerOne, IPlayer playerTwo)
        {
            this.Board = playingBoard;
            this.PlayerOne = playerOne;
            this.PlayerTwo = playerTwo;
            this.PlayerTurn = TokenColour.Red;
            this.CalculateMaxPossibleMoves();
        } 

        private void CalculateMaxPossibleMoves()
        {
            this.MaxPossibleMoves = this.Board.PlayingBoard.GetLength(0) * this.Board.PlayingBoard.GetLength(1);
        }

        private int FindAvailableRow(int col)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (this.Board.PlayingBoard[row, col].color == TokenColour.Blank)
                {
                    return row;
                }
            }
            throw new ArgumentOutOfRangeException("Kolom is vol, voer een kolom in die nog niet vol zit.");
        }

        public int RegisterMoveToBoard(int move)
        {
            try
            {
                int row = FindAvailableRow(move);
                this.Board.PlayingBoard[row, move].color = this.PlayerTurn;
                this.MaxPossibleMoves--;
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public void SwapPlayerTurns()
        {
            if (this.PlayerTurn == TokenColour.Red)
            {
                this.PlayerTurn = TokenColour.Yellow;
            }
            else
            {
                this.PlayerTurn = TokenColour.Red;
            }
        }

        // The first nested for loop checks diagonals from the top-left to the bottom-right
        // The second nested for loop checks diagonals from the top-right to the bottom-left
        private bool CheckDiagonals(int rows, int columns)
        {
            for (int row = 0; row < rows - 3; row++)
            {
                for (int col = 0; col < columns - 3; col++)
                {
                    if (this.Board.PlayingBoard[row, col].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row + 1, col + 1].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row + 2, col + 2].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row + 3, col + 3].color == this.PlayerTurn)
                    {
                        return true;
                    }
                }
            }

            for (int row = 0; row < rows - 3; row++)
            {
                for (int col = columns - 1; col >= 3; col--)
                {
                    if (this.Board.PlayingBoard[row, col].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row + 1, col - 1].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row + 2, col - 2].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row + 3, col - 3].color == this.PlayerTurn)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckColumns(int rows, int columns)
        {
            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows - 3; row++)
                {
                    if (this.Board.PlayingBoard[row, col].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row + 1, col].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row + 2, col].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row + 3, col].color == this.PlayerTurn)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckRows(int rows, int columns)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns - 3; col++)
                {
                    if (this.Board.PlayingBoard[row, col].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row, col + 1].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row, col + 2].color == this.PlayerTurn &&
                        this.Board.PlayingBoard[row, col + 3].color == this.PlayerTurn)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckForWinner()
        {
            int rows = this.Board.PlayingBoard.GetLength(0);
            int columns = this.Board.PlayingBoard.GetLength(1);

            if (CheckRows(rows, columns))
                return (true);

            if (CheckColumns(rows, columns))
                return (true);

            if (CheckDiagonals(rows, columns))
                return (true);

            return (false);
        }

        public TokenColour PlayerTurn { get; set; }
        public int MaxPossibleMoves { get; set; }
        public IBoard Board { get; }
        private IPlayer PlayerOne { get; }
        private IPlayer PlayerTwo { get; }
    }
}
