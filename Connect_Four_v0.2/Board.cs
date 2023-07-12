using ConnectFourLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_Four_v0._2
{
    internal class Board : IBoard
    {
        public Board(int height, int width)
        {
            this.PlayingBoard = new int[height, width];
        }

        public int[,] PlayingBoard { get; }
    }
}
