using ConnectFourLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Tests
{
    internal class GameTests
    {
        [TestMethod()]
        public void switchPlayerTurnTest()
        {
            IPlayer playerOne = new Player("Yes");
            IPlayer playerTwo = new Player("No");
        }
    }
}
