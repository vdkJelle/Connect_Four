using Microsoft.AspNetCore.SignalR;
using ConnectFourWeb.Data;

namespace ConnectFourWeb.Hubs
{
    public class MatchmakingHub: Hub
    {
        private readonly static Dictionary<string, string> connectionIds = new();
        private static string? otherUser;

        public override async Task OnConnectedAsync()
        {
            string userId = Guid.NewGuid().ToString();

            connectionIds[userId] = Context.ConnectionId;

            await base.OnConnectedAsync();
        }

        private static string GenerateString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random randomStringGenerator = new();

            int length = 8;

            string randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[randomStringGenerator.Next(s.Length)]).ToArray());

            return randomString;
        }

        public async Task InitiateMatchmaking()
        {
            if (string.IsNullOrEmpty(otherUser))
            {
                otherUser = Context.ConnectionId;
                return;
            }
            if (string.Equals(otherUser, Context.ConnectionId))
            {
                return;
            }

            string gameId = GenerateString();
            GameService.InitialiseGameData(gameId, otherUser, Context.ConnectionId);
            string url = $"/game/{gameId}";
            await Clients.Clients(Context.ConnectionId, otherUser).SendAsync("InitiateMatch", url);
        }

        private async void NotifyOtherClients(string key) => await Clients.All.SendAsync("UserDisconnected", key);

        private void RemoveClientFromQueue()
        {
            KeyValuePair<string, string> user = connectionIds.FirstOrDefault(x => x.Value == Context.ConnectionId);

            if (user.Value == otherUser)
            {
                otherUser = "";
            }
        }

        private static void RemoveUserFromDictionary(KeyValuePair<string, string> user) => connectionIds.Remove(user.Key);

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            KeyValuePair<string, string> user = connectionIds.FirstOrDefault(x => x.Value == Context.ConnectionId);

            if (!string.IsNullOrEmpty(user.Key))
            {
                NotifyOtherClients(user.Key);
                RemoveClientFromQueue();
                RemoveUserFromDictionary(user);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
