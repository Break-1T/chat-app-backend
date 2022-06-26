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

        /// <summary>
        /// Gets the list groups asynchronous.
        /// </summary>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>ApiOperationResult.</returns>
        Task<ApiOperationResult<List<Group>>> GetGroupsAsync(Guid currentUserId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the group asynchronous.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>ApiOperationResult.</returns>
        Task<ApiOperationResult<Group>> GetGroupAsync(Guid groupId, CancellationToken cancellationToken = default);
        Task<ApiOperationResult<Group>> LeaveGroupAsync(Guid currentUserId, Guid groupId, CancellationToken cancellationToken);
    }
}
