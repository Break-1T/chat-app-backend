using Chat.Db.Models;
using Chat.Db.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Chat.Db.Test
{
    public class GroupStoreTests
    {
        private readonly ChatDbContext _inMemoryChatDbContext;
        private readonly GroupStore _groupStore;

        public GroupStoreTests()
        {
            var options = new DbContextOptionsBuilder<ChatDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            this._inMemoryChatDbContext = new ChatDbContext(options.Options);

            this._groupStore = new GroupStore(Mock.Of<ILogger<GroupStore>>(), this._inMemoryChatDbContext);
        }

        [Fact]
        public async void GetGroupsAsync_Test()
        {
            var user = new AppIdentityUser();

            this._inMemoryChatDbContext.Add(user);
            await this._inMemoryChatDbContext.SaveChangesAsync();

            var group = new Group
            {
                CreatedByUser = user.Id,
                Name = "Test_Group",
                UserGroups = new List<UserGroup>
                {
                    new UserGroup
                    {
                        UserId = user.Id,
                    },
                },
            };
            
            this._inMemoryChatDbContext.Add(group);
            await this._inMemoryChatDbContext.SaveChangesAsync();

            var result = await this._groupStore.GetGroupsAsync(user.Id);
        }

        [Fact]
        public async void GetGroupAsync_Test()
        {
            var user = new AppIdentityUser();

            this._inMemoryChatDbContext.Add(user);
            await this._inMemoryChatDbContext.SaveChangesAsync();

            var group = new Group
            {
                CreatedByUser = user.Id,
                Name = "Test_Group",
                UserGroups = new List<UserGroup>
                {
                    new UserGroup
                    {
                        UserId = user.Id,
                    },
                },
            };

            this._inMemoryChatDbContext.Add(group);
            await this._inMemoryChatDbContext.SaveChangesAsync();

            var result = await this._groupStore.GetGroupAsync(group.GroupId);
        }
    }
}