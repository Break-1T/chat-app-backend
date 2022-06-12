using AutoMapper;
using Chat.Api.Constants;
using Chat.Api.Interfaces;
using Chat.Api.Models.Groups;
using Chat.Api.ResultModels;
using Chat.Db.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Services
{
    public class GroupService : IGroupService
    {
        private readonly ILogger<GroupService> _logger;
        private readonly IMapper _mapper;
        private readonly IGroupStore _groupStore;

        public GroupService(ILogger<GroupService> logger, IMapper mapper, IGroupStore groupStore)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._groupStore = groupStore ?? throw new ArgumentNullException(nameof(groupStore));
        }

        /// <inheritdoc/>
        public async Task<ApiOperationResult<Group>> CreateGroupAsync(Group createGroupRequest, Guid currentUserId, CancellationToken cancellationToken = default)
        {
            try
            {
                var dbGroup = this._mapper.Map<Db.Models.Group>(createGroupRequest);
                dbGroup.CreatedByUser = currentUserId;
                dbGroup.UserGroups = new List<Db.Models.UserGroup>
                {
                    new Db.Models.UserGroup
                    {
                        UserId = currentUserId,
                    }
                };

                var createGroupResult = await this._groupStore.AddGroupAsync(dbGroup, cancellationToken);

                if (!createGroupResult.IsSuccess)
                {
                    var error = new SerializableError { { createGroupResult.Status.ToString(), createGroupResult.ErrorMessage } };
                    return ApiOperationResult<Group>.FromError(error);
                }

                return ApiOperationResult<Group>.FromSuccess(createGroupRequest);
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.CreateGroupUnexpectedError, ex, ex.Message);
                return ApiOperationResult<Group>.FromError(new SerializableError { { "UnexpectedError", "UnexpectedError" } });
            }
        }
    }
}
