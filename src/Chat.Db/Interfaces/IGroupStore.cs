using Chat.Db.Models;

namespace Chat.Db.Interfaces 
{
    public interface IGroupStore
    {
        /// <summary>
        /// Gets the group asynchronous.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"> containing <see cref="Group"/> of operation.</returns>
        Task<Group> GetGroupAsync(Guid groupId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the group asynchronous.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"> containing <see cref="Group"/> of operation.</returns>
        Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the group asynchronous.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"> containing <see cref="Group"/> of operation.</returns>
        Task<Group> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default);
    }
}
