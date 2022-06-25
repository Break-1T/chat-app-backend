using AutoMapper;
using Chat.Api.Constants;
using Chat.Api.Hubs;
using Chat.Api.Interfaces;
using Chat.Api.Models.Users;
using Chat.Api.ResultModels;
using Chat.Db.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Services
{
    public class ChatService : IChatService
    {
        private readonly ILogger<ChatService> _logger;
        private readonly IUserGroupStore _userGroupStore;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMapper _mapper;

        public ChatService(ILogger<ChatService> logger, IUserGroupStore userGroupStore, IHubContext<ChatHub> hubContext, IMapper mapper)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._userGroupStore = userGroupStore ?? throw new ArgumentNullException(nameof(userGroupStore));
            this._hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<ApiOperationResult<User>> TryConnectAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default)
        {
            try
            {
                var getUserGroupResult = await this._userGroupStore.GetUserGroupAsync(userId, groupId, cancellationToken);

                if (!getUserGroupResult.IsSuccess)
                {
                    var error = new SerializableError { { getUserGroupResult.Status.ToString(), getUserGroupResult.ErrorMessage } };
                    return ApiOperationResult<User>.FromError(error);
                }

                var result = this._mapper.Map<User>(getUserGroupResult.Entity.User);

                return ApiOperationResult<User>.FromSuccess(result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.TryConnectUnexpectedError, ex, ex.Message);
                return ApiOperationResult<User>.FromError(new SerializableError { { "UnexpectedError", "UnexpectedError" } });
            }
        }
    }
}
