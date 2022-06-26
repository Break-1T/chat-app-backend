using Chat.Db.Constants;
using Chat.Db.Interfaces;
using Chat.Db.Models;
using Chat.Db.ResultModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat.Db.Stores
{
    public class UserGroupStore : IUserGroupStore
    {
        private readonly ILogger<UserGroupStore> _logger;
        private readonly ChatDbContext _dbContext;

        public UserGroupStore(ILogger<UserGroupStore> logger, ChatDbContext dbContext)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public async Task<DbOperationResult<UserGroup>> GetUserGroupAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this._dbContext.UserGroups.AsNoTracking()
                    .Include(ug => ug.User)
                    .FirstOrDefaultAsync(ug => ug.GroupId == groupId && ug.UserId == userId, cancellationToken);

                return result == null 
                    ? DbOperationResult<UserGroup>.FromError("NotFound")
                    : DbOperationResult<UserGroup>.FromSuccess(result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.GetUserGroupUnexpectedError, ex, 
                    $"UserGroup not found.\n" +
                    $"UserId: '{userId}'\n" +
                    $"GroupId: '{groupId}'");
                return DbOperationResult<UserGroup>.FromError("UnexpectedError");
            }
        }

        /// <inheritdoc/>
        public async Task<DbOperationResult<UserGroup>> DeleteUserGroupAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default)
        {
            var userGroup = new UserGroup { GroupId = groupId, UserId = userId };

            try
            {
                this._dbContext.Attach(userGroup);
                this._dbContext.Remove(userGroup);

                var result = await this._dbContext.SaveChangesAsync(cancellationToken);

                return result == 0
                    ? DbOperationResult<UserGroup>.FromError("Error")
                    : DbOperationResult<UserGroup>.FromSuccess(null);
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.GetUserGroupUnexpectedError, ex,
                    $"UserGroup not found.\n" +
                    $"UserId: '{userId}'\n" +
                    $"GroupId: '{groupId}'");
                return DbOperationResult<UserGroup>.FromError("UnexpectedError");
            }
        }
    }
}
