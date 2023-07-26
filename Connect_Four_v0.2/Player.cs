using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourLibrary
{
    public class Player : IPlayer
    {
        public Player(string playerId)
        {
            this._playerId = playerId;
        }

        public string PlayerId 
        { 
            get { return (_playerId); }
        }

        private string _playerId;
    }
}
