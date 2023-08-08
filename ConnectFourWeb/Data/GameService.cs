using ConnectFourLibrary;
using ConnectFourWeb.Models;

namespace ConnectFourWeb.Data
{
    public class GameService
    {
        private static readonly ConnectFourWebContext? _context;

        public static void InitialiseGameData(string gameId, string playerOneId, string playerTwoId)
        {
            IBoard board = new Board(6, 7);
            IPlayer playerOne = new Player(playerOneId);
            IPlayer playerTwo = new Player(playerTwoId);
            IGame game = new Game(board, playerOne, playerTwo);

            var dbGame = new DbGame
            {
                Id = gameId,
                GameManager = (Game)game,
                PlayerOne = (Player)playerOne,
                PlayerTwo = (Player)playerTwo,
            };

            _context.Games.Add(dbGame);
            _context.SaveChanges();
        }

        public static IGame GetGameManagerById(string gameId)
        {
            var dbGame = _context.Games.FirstOrDefault(g => g.Id == gameId);
            return dbGame?.GameManager;
        }
    }
}
