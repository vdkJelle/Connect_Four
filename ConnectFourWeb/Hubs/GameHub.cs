using ConnectFourLibrary;
using ConnectFourWeb.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;

namespace ConnectFourWeb.Hubs
{
    public class GameHub: Hub
    {
        private readonly static Dictionary<string, string> connectionIds = new();
        private static Dictionary<string, string[]> connectionCount = new();

        public override async Task OnConnectedAsync()
        {
            string userId = Guid.NewGuid().ToString();

            connectionIds[userId] = Context.ConnectionId;

            await base.OnConnectedAsync();
        }

        public async Task AddUserToGroup(string gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        }
 
        public async Task SetPlayers(string gameId)
        {
            if (connectionCount.ContainsKey(gameId))
            {
                connectionCount[gameId] = connectionCount[gameId].Append(Context.ConnectionId).ToArray();
                if (connectionCount[gameId].Length == 2)
                {
                    await Clients.Clients(Context.ConnectionId).SendAsync("SetPlayerTwo", Context.ConnectionId);
                }
            }
            else
            {
                connectionCount[gameId] = new[] { Context.ConnectionId };
                await Clients.Clients(Context.ConnectionId).SendAsync("SetPlayerOne", Context.ConnectionId);
            }
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
