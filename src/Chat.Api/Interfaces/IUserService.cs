using Chat.Api.Models;
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
        /// Signs up asynchronous.
        /// </summary>
        /// <param name="createUserRequest">The create user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// ApiOperationResult.
        /// </returns>
        Task<ApiOperationResult<User>> SignUpAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default);


        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="loginUserRequest">The login user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>TokenResponse.</returns>
        Task<ApiOperationResult<TokenResponse>> LoginAsync(LoginUserRequest loginUserRequest, CancellationToken cancellationToken = default);

        Task<ApiOperationResult<List<User>>> GetUsersAsync(Guid currentUserId, CancellationToken cancellationToken = default);
    }
}
