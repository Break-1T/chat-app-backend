using Chat.Api.Constants;
using Chat.Api.Hubs;
using Chat.Api.Interfaces;
using Chat.Api.ResultModels;
using Chat.Db.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Services
{
    public class ChatService : IChatService
    {
        private readonly ILogger<ChatService> _logger;
        private readonly UserGroupStore _userGroupStore;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(ILogger<ChatService> logger, UserGroupStore userGroupStore, IHubContext<ChatHub> hubContext)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._userGroupStore = userGroupStore ?? throw new ArgumentNullException(nameof(userGroupStore));
            this._hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        /// <inheritdoc/>
        public async Task<ApiOperationResult<bool>> TryConnectAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this._userGroupStore.GetUserGroupAsync(userId, groupId, cancellationToken);

                if (!result.IsSuccess)
                {
                    var error = new SerializableError { { result.Status.ToString(), result.ErrorMessage } };
                    return ApiOperationResult<bool>.FromError(error);
                }

                return ApiOperationResult<bool>.FromSuccess(true);
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.TryConnectUnexpectedError, ex, ex.Message);
                return ApiOperationResult<bool>.FromError(new SerializableError { { "UnexpectedError", "UnexpectedError" } });
            }
        }
    }
}
