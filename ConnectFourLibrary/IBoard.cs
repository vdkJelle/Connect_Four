﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourLibrary
{
    public interface IBoard
    {
        public GameToken[,] PlayingBoard { get; }
    }
}
