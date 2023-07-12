using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourLibrary
{
    public class Player : IPlayer
    {
        public Player(int playerId)
        {
            this._playerId = playerId;
        }

        public bool HasActiveTurn
        {
            get { return (_hasActiveTurn); }
            set { _hasActiveTurn = value; }
        }
        public int PlayerId 
        { 
            get { return (_playerId); }
        }

        private bool _hasActiveTurn;
        private int _playerId;
    }
}
