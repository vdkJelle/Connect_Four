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
            IPlayer playerOne = new Player(1);
            IPlayer playerTwo = new Player(2);

            playerOne.HasActiveTurn = true;
            playerTwo.HasActiveTurn = false;
            Assert.IsTrue(playerOne.HasActiveTurn);

            playerTwo.HasActiveTurn = true;
            playerOne.HasActiveTurn = false;
            Assert.IsTrue(playerTwo.HasActiveTurn);
        }
    }
}
