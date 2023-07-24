using ConnectFourLibrary;
using Microsoft.AspNetCore.ResponseCompression;

namespace ConnectFourWeb.Data
{
    internal class Board: IBoard
    {
        public Board(int height, int width)
        {
            this.PlayingBoard = new GameToken[height, width];
            InitialiseBoard();
        }

        public void InitialiseBoard()
        {
            for (int i = 0; i < PlayingBoard.GetLength(0); i++)
            {
                for (int j = 0; j < PlayingBoard.GetLength(1); j++)
                {
                    PlayingBoard[i, j] = new GameToken(TokenColour.Blank);
                }
            }
        }

        public GameToken[,] PlayingBoard { get; }
    }
}
