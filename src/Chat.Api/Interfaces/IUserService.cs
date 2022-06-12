using Chat.Api.Models.Users;
using Chat.Api.ResultModels;

namespace Chat.Api.Interfaces
{
    /// <summary>
    /// IUserService.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="createUserRequest">The create user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>ApiOperationResult.</returns>
        Task<ApiOperationResult<User>> CreateUserAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default);
    }
}
