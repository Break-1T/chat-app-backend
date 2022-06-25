using Chat.Api.Extensions;
using Chat.Api.Hubs;
using Chat.Api.Interfaces;
using Chat.Api.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Chat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            this._chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
        }

        /// <summary>
        /// Try to connect to group.
        /// </summary>
        /// <param name="createGroupRequest">The create group request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("{groupId:guid}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SerializableError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> TryConnect(Guid groupId, CancellationToken cancellationToken = default)
        {
            var currentUserId = this.User.GetUserId();
            var connectResult = await this._chatService.TryConnectAsync(currentUserId, groupId, cancellationToken);

            if (!connectResult.IsSuccess)
            {
                return this.BadRequest(connectResult.Error);
            }

            return this.Ok(connectResult.Entity);
        }
    }
}
