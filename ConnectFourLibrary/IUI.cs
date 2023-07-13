using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourLibrary
{
    public interface IUI
    {
        void DrawBoard(IBoard board);
        int UserInput();
        void ClearConsole();
        void HandleFullColumn();
        void AnnounceResult(IGame gameManager);
        void Rematch(ref bool replay);
        void IntroductionMessage();
        void Rules();
    }
}
