using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using ConnectFourWeb.Data;
using Microsoft.AspNetCore.Authorization;

namespace ConnectFourWeb.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MatchmakingHub: Hub
    {
        private readonly static Dictionary<string, string> connectionIds = new();
        private static string? otherUserId;
        private static string? otherUserConnectionId;

        public override async Task OnConnectedAsync()
        {
            var currentUser = Context.User;
            
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
            var currentUser = Context.User;

            if (string.IsNullOrEmpty(otherUserId))
            {
                otherUserId = currentUser.FindFirst(ClaimTypes.Name).Value;
                otherUserConnectionId = Context.ConnectionId;
                await Clients.Clients(Context.ConnectionId).SendAsync("Queued");
                return;
            }
            if (string.Equals(otherUserId, currentUser.FindFirst(ClaimTypes.Name).Value))
            {
                await Clients.Clients(Context.ConnectionId).SendAsync("Queue_Duplicate");
                return;
            }

            string gameId = GenerateString();
            GameService.InitialiseGameData(gameId, otherUserId, currentUser.FindFirst(ClaimTypes.Name).Value);
            string url = $"/game/{gameId}";
            await Clients.Clients(Context.ConnectionId, otherUserConnectionId).SendAsync("InitiateMatch", url);
        }

        private async void NotifyOtherClients(string key) => await Clients.All.SendAsync("UserDisconnected", key);

        private void RemoveClientFromQueue()
        {
            KeyValuePair<string, string> user = connectionIds.FirstOrDefault(x => x.Value == Context.ConnectionId);

            if (user.Value == otherUserConnectionId)
            {
                otherUserConnectionId = "";
                otherUserId = "";
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
