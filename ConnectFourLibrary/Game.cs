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
            this._board = playingBoard;
            this._playerOne = playerOne;
            this._playerTwo = playerTwo;
            this.PlayerTurn = TokenColour.Red;
            this.InitialiseBoard();
            this.CalculateMaxPossibleMoves();
        } 

        private void CalculateMaxPossibleMoves()
        {
            this.MaxPossibleMoves = this._board.PlayingBoard.GetLength(0) * this._board.PlayingBoard.GetLength(1);
        }

        public void InitialiseBoard()
        {
            for (int i = 0; i < this._board.PlayingBoard.GetLength(0); i++)
            {
                Array.Clear(this._board.PlayingBoard, 0, this._board.PlayingBoard.GetLength(1));
            }
        } 

        private int FindAvailableRow(int col)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (this._board.PlayingBoard[row, col].color == TokenColour.Blank)
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
                this._board.PlayingBoard[row, move].color = this.PlayerTurn;
                return 0;
            }
            catch (ArgumentOutOfRangeException e)
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
                    if (this._board.PlayingBoard[row, col].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row + 1, col + 1].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row + 2, col + 2].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row + 3, col + 3].color == this.PlayerTurn)
                    {
                        return true;
                    }
                }
            }

            for (int row = 0; row < rows - 3; row++)
            {
                for (int col = columns - 1; col >= 3; col--)
                {
                    if (this._board.PlayingBoard[row, col].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row + 1, col - 1].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row + 2, col - 2].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row + 3, col - 3].color == this.PlayerTurn)
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
                    if (this._board.PlayingBoard[row, col].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row + 1, col].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row + 2, col].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row + 3, col].color == this.PlayerTurn)
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
                    if (this._board.PlayingBoard[row, col].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row, col + 1].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row, col + 2].color == this.PlayerTurn &&
                        this._board.PlayingBoard[row, col + 3].color == this.PlayerTurn)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckConnectedFour()
        {
            int rows = this._board.PlayingBoard.GetLength(0);
            int columns = this._board.PlayingBoard.GetLength(1);

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
        public IBoard Board
        {
            get { return this._board; }
        }

        private IBoard _board;
        private IPlayer _playerOne;
        private IPlayer _playerTwo;
    }
}
