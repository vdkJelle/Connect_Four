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
            set { _playerId = value; }
        }

        public TokenColour PlayerColour { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private string _playerId;
        
    }
}
