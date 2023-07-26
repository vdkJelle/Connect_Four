using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFourLibrary;

namespace ConnectFourWeb.Data
{
    public class Player : IPlayer
    {
        public Player(string playerId)
        {
            PlayerId = playerId;
        }

        public string PlayerId { get; set; }
        public TokenColour PlayerColour { get; set; }
    }
}
