using ConnectFourLibrary;
using Microsoft.AspNetCore.ResponseCompression;

namespace ConnectFourWeb.Data
{
    internal class Board: IBoard
    {
        public Board(int height, int width)
        {
            this.PlayingBoard = new GameToken[height, width]; 
        }

        public void InitialiseBoard()
        {
            for (int i = 0; i < PlayingBoard.GetLength(0); i++)
            {
                Array.Clear(PlayingBoard, 0, PlayingBoard.GetLength(1));
            }
        }

        public GameToken[,] PlayingBoard { get; }
    }
}
