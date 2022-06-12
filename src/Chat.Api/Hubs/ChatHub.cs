using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string groupId, string message)
        {
            await Clients.OthersInGroup(groupId).SendAsync(message);
        }
    }
}
