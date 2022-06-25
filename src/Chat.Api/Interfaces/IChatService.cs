using Chat.Api.ResultModels;

namespace Chat.Api.Interfaces
{
    public interface IChatService
    {
        Task<ApiOperationResult<bool>> TryConnectAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default);
    }
}
