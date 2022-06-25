using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Hubs
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private const string SendMessageName = "SendMessage";

        private Guid GroupId
        {
            get
            {
                var segments = this.Context.GetHttpContext().Request.Path.ToString().Split("/").ToList();

                if (segments.Count != 3 || !Guid.TryParse(segments[2], out var groupId))
                {
                    throw new ArgumentException("Incorect groupId");
                }

                return groupId;
            }
        }

        public async Task SendMessage(object user, string message)
        {
            await Clients.Group(GroupId.ToString()).SendAsync(SendMessageName, user, message);
        }

        /// <summary>
        /// Called when a new connection is established with the hub.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task that represents the asynchronous connect.</returns>
        public override async Task OnConnectedAsync()
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, this.GroupId.ToString());
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Called when [disconnected asynchronous].
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>A System.Threading.Tasks.Task that represents the asynchronous disconnect.</returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, this.GroupId.ToString());
            await base.OnDisconnectedAsync(ex);
        }
    }
}
