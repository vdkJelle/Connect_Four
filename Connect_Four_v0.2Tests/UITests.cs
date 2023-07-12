using App;
using Connect_Four_v0._2;
using ConnectFourLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_Four_v0._2Tests
{
    public static class Constants
    {
        public static readonly string INTRODUCTION = "Welcome to\r\n  ______   ______   .__   __. .__   __.  _______   ______ .___________.    _  _\r\n /      | /  __  \\  |  \\ |  | |  \\ |  | |   ____| /      ||           |   | || |\r\n|  ,----'|  |  |  | |   \\|  | |   \\|  | |  |__   |  ,----'`---|  |----`   | || |_\r\n|  |     |  |  |  | |  . `  | |  . `  | |   __|  |  |         |  |        |__   _|\r\n|  `----.|  `--'  | |  |\\   | |  |\\   | |  |____ |  `----.    |  |           | |\r\n \\______| \\______/  |__| \\__| |__| \\__| |_______| \\______|    |__|           |_|\r\n\r\nPress any key to begin";
        public static readonly string RULES = "How to Play:\r\n1. Choose who plays first. Player 1 will be yellow and Player 2 will be red.\r\n2. Players will alternate taking turns in putting their tokens in the board. You will put your token in the board by typing in the number at the top of the column you wish to put it in.\r\n3. The first player to connect four tokens in a row wins. The four in a row can be horizontal, vertical or diagonal.\r\n\r\nPress a Key to begin.";
    }

    internal class UITests
    {
        [TestMethod]
        public void IntroductionMessageTest()
        {
            IUI consoleUI = new ConsoleUI();

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            consoleUI.IntroductionMessage();
            Assert.AreEqual(stringWriter.ToString(), Constants.INTRODUCTION);
        }

        [TestMethod]
        public void RulesMessageTest()
        {
            IUI consoleUI = new ConsoleUI();

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            consoleUI.IntroductionMessage();
            Assert.AreEqual(stringWriter.ToString(), Constants.RULES);
        }

        [TestMethod]
        public void UserInputTest()
        {
            
        }
    }
}
