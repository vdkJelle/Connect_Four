using ConnectFourLibrary;
using ConnectFourWeb.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;

namespace ConnectFourWeb.Hubs
{
    public class GameHub: Hub
    {
        private readonly static Dictionary<string, string> connectionIds = new();
        private readonly static IBoard board = new Board(6, 7);
        private static IPlayer? playerOne;
        private static IPlayer? playerTwo;
        private static List<IGame> gameManager = new();
        NavigationManager _navigationManager;

        public GameHub(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public override async Task OnConnectedAsync()
        {
            string userId = Guid.NewGuid().ToString();

            connectionIds[userId] = Context.ConnectionId;

            await base.OnConnectedAsync();
        }

        public async Task GetGameManagerById(string id)
        {
            await Clients.Group(id).SendAsync("GetGameManager", gameManager.First());
        }

        public async Task StartGame(string gameId, string otherUser)
        {
            playerOne = new Player(otherUser);
            playerTwo = new Player(Context.ConnectionId);
            gameManager.Add(new Game(board, playerOne, playerTwo));
            string url = $"/game/{gameId}";

            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Groups.AddToGroupAsync(otherUser, gameId);
        }

        private async void NotifyOtherClients(string key) => await Clients.All.SendAsync("UserDisconnceted", key);

        private static void RemoveUserFromDictionary(KeyValuePair<string, string> user) => connectionIds.Remove(user.Key);

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            KeyValuePair<string, string> user = connectionIds.FirstOrDefault(x => x.Value == Context.ConnectionId);

            if (!string.IsNullOrEmpty(user.Key))
            {
                NotifyOtherClients(user.Key);
                RemoveUserFromDictionary(user);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
