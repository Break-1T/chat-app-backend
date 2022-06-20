using Chat.Db.Models;
using Chat.Db.ResultModels;

namespace Chat.Db.Interfaces 
{
    /// <summary>
    /// IGroupStore.
    /// </summary>
    public interface IGroupStore
    {
        /// <summary>
        /// Gets the group asynchronous.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"> containing <see cref="DbOperationResult{TEntity}"/> of operation.</returns>
        Task<DbOperationResult<Group>> GetGroupAsync(Guid groupId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the groups asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"> containing <see cref="DbOperationResult{TEntity}"/> of operation.</returns>
        Task<DbOperationResult<List<Group>>> GetGroupsAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the group asynchronous.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"> containing <see cref="DbOperationResult{TEntity}"/> of operation.</returns>
        Task<DbOperationResult<Group>> AddGroupAsync(Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the group asynchronous.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"> containing <see cref="DbOperationResult{TEntity}"/> of operation.</returns>
        Task<DbOperationResult<Group>> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default);
    }
}
