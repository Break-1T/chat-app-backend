using Chat.Api.Models.Groups;
using Chat.Api.ResultModels;

namespace Chat.Api.Interfaces
{
    /// <summary>
    /// IGroupService.
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Creates the group asynchronous.
        /// </summary>
        /// <param name="createGroupRequest">The create group request.</param>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// ApiOperationResult.
        /// </returns>
        Task<ApiOperationResult<Group>> CreateGroupAsync(Group createGroupRequest, Guid currentUserId, CancellationToken cancellationToken = default);
    }
}
