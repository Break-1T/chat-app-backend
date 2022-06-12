using Chat.Db.Interfaces;
using Chat.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        public async Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            try
            {
                this._dbContext.Groups.Add(group);
                var result = await this._dbContext.SaveChangesAsync(cancellationToken);

                return result == 0 ? null : group;
            }
            catch (Exception ex)
            {
                this._logger.LogError(new EventId(), ex, null);
                this._dbContext.Entry(group).State = EntityState.Detached;
                throw;
            }        
        }

        /// <inheritdoc/>
        public async Task<Group> GetGroupAsync(Guid groupId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this._dbContext.Groups.AsNoTracking()
                    .Include(g => g.UserGroups).ThenInclude(ug => ug.Group)
                    .Include(g => g.UserGroups).ThenInclude(ug => ug.User)
                    .FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);

                return result == null ? null : result;
            }
            catch (Exception ex)
            {
                this._logger.LogError(new EventId(), ex, null);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Group> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default)
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

                return result == 0 ? null : group;
            }
            catch (Exception ex)
            {
                this._logger.LogError(new EventId(), ex, null);
                this._dbContext.Entry(dbGroup).State = EntityState.Detached;
                throw;
            }
        }
    }
}
