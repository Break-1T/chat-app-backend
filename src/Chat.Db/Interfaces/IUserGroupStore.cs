using Chat.Db.Models;
using Chat.Db.ResultModels;

namespace Chat.Db.Interfaces
{
    public interface IUserGroupStore
    {   
        /// <summary>
        /// Gets UserGroup async.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>DbOperationResult>.</returns>
        Task<DbOperationResult<UserGroup>> GetUserGroupAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default);
    }
}
