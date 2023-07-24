using ConnectFourLibrary;

namespace ConnectFourWeb.Data
{
    public class GameService
    {
        private static Dictionary<string, IGame> gameManagers = new();

        public static void InitialiseGameData(string gameId, string playerOneId, string playerTwoId)
        {
            IBoard board = new Board(6, 7);
            IPlayer playerOne = new Player(playerOneId);
            IPlayer playerTwo = new Player(playerTwoId);
            gameManagers[gameId] = new Game(board, playerOne, playerTwo);
        }

        public static IGame GetGameManagerById(string gameId)
        {
            return gameManagers[gameId];
        }
    }
}
