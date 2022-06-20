using Chat.Db.Interfaces;
using Chat.Db.Models;
using Chat.Db.ResultModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Chat.Db.Constants;

namespace Chat.Db.Stores
{
    public class GroupStore : IGroupStore
    {
        private readonly ILogger<GroupStore> _logger;
        private readonly ChatDbContext _dbContext;

        public GroupStore(ILogger<GroupStore> logger, ChatDbContext dbContext)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public async Task<DbOperationResult<Group>> AddGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            try
            {
                this._dbContext.Groups.Add(group);
                var result = await this._dbContext.SaveChangesAsync(cancellationToken);

                return result == 0 
                    ? DbOperationResult<Group>.FromError("DbError") 
                    : DbOperationResult<Group>.FromSuccess(group);
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.AddGroupUnexpectedError, ex, null, group);
                this._dbContext.Entry(group).State = EntityState.Detached;
                return DbOperationResult<Group>.FromError("UnexpectedError");
            }        
        }

        /// <inheritdoc/>
        public async Task<DbOperationResult<Group>> GetGroupAsync(Guid groupId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this._dbContext.Groups.AsNoTracking()
                    .Include(g => g.UserGroups).ThenInclude(ug => ug.User)
                    .FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);

                return result == null 
                    ? DbOperationResult<Group>.FromError("DbError")
                    : DbOperationResult<Group>.FromSuccess(result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.GetGroupUnexpectedError, ex, null, groupId);
                return DbOperationResult<Group>.FromError("UnexpectedError");
            }
        }

        /// <inheritdoc/>
        public async Task<DbOperationResult<List<Group>>> GetGroupsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this._dbContext.UserGroups.AsNoTracking()
                    .Include(ug => ug.Group)
                    .Where(ug => ug.UserId == userId)
                    .Select(ug => ug.Group)
                    .ToListAsync(cancellationToken);

                return DbOperationResult<List<Group>>.FromSuccess(result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.GetGroupsUnexpectedError, ex, null, userId);
                return DbOperationResult<List<Group>>.FromError("UnexpectedError");
            }
        }

        /// <inheritdoc/>
        public async Task<DbOperationResult<Group>> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            Group dbGroup = null;

            try
            {
                dbGroup = await this._dbContext.Groups
                    .Include(g => g.UserGroups).ThenInclude(ug => ug.Group)
                    .Include(g => g.UserGroups).ThenInclude(ug => ug.User)
                    .FirstOrDefaultAsync(g => g.GroupId == group.GroupId, cancellationToken);

                this._dbContext.Entry(dbGroup).CurrentValues.SetValues(group);
                dbGroup.UserGroups = group.UserGroups;

                dbGroup.RecModified = DateTime.UtcNow;

                var result = await this._dbContext.SaveChangesAsync(cancellationToken);

                return result == 0
                    ? DbOperationResult<Group>.FromError("DbError")
                    : DbOperationResult<Group>.FromSuccess(dbGroup);
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.UpdateGroupUnexpectedError, ex, null, group);
                this._dbContext.Entry(dbGroup).State = EntityState.Detached;
                return DbOperationResult<Group>.FromError("UnexpectedError");
            }
        }
    }
}
