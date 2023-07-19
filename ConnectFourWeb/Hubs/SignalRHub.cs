﻿using Microsoft.AspNetCore.SignalR;

namespace ConnectFourWeb.Hubs
{
    public class SignalRHub: Hub
    {
        private readonly static Dictionary<string, string> connectionIds = new();

        public override async Task OnConnectedAsync()
        {
            string username = Context.User.Identity.Name;

            string userId = Guid.NewGuid().ToString();

            connectionIds[userId] = Context.ConnectionId;

            await Clients.Caller.SendAsync("User Connected", userId);

            await Clients.All.SendAsync("NewUserConnected", username, userId);

            await base.OnConnectedAsync();
        }

        private async void NotifyOtherClients(string key) => await Clients.All.SendAsync("UserDisconnected", key);

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
