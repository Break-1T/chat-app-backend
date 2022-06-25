using Chat.Api.Models.Users;
using Chat.Api.ResultModels;

namespace Chat.Api.Interfaces
{
    public interface IChatService
    {
        Task<ApiOperationResult<User>> TryConnectAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default);
    }
}
