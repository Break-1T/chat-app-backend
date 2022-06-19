using AutoMapper;
using Chat.Api.Constants;
using Chat.Api.Interfaces;
using Chat.Api.Models;
using Chat.Api.Models.Users;
using Chat.Api.Options;
using Chat.Api.ResultModels;
using Chat.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using DbConstants = Chat.Db.Constants.DbConstants;

namespace Chat.Api.Services
{
    /// <summary>
    /// UserService.
    /// </summary>
    /// <seealso cref="Chat.Api.Interfaces.IUserService" />
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly HttpClient _httpClient;
        private readonly AuthOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="System.ArgumentNullException">
        /// logger
        /// or
        /// mapper
        /// or
        /// userManager
        /// or
        /// httpClient
        /// or
        /// options.
        /// </exception>
        public UserService(ILogger<UserService> logger, IMapper mapper, UserManager<AppIdentityUser> userManager, HttpClient httpClient, IOptions<AuthOptions> options)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this._options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc/>
        public async Task<ApiOperationResult<User>> SignUpAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var dbUser = this._mapper.Map<AppIdentityUser>(createUserRequest);
                dbUser.UserRoles = new List<AppIdentityUserRole>
                {
                    new AppIdentityUserRole
                    {
                        RoleId = DbConstants.Roles.First(r => r.Key == DbConstants.UserRoleName).Value,
                    }
                };
                dbUser.UserGroups = null;
                dbUser.LockoutEnabled = false;
                dbUser.EmailConfirmed = true;

                var createUserResult = await this._userManager.CreateAsync(dbUser, createUserRequest.Password);

                if (!createUserResult.Succeeded)
                {
                    var errors = new SerializableError();
                    foreach (var error in createUserResult.Errors)
                    {
                        errors.Add(error.Code, error.Description);
                    }
                    return ApiOperationResult<User>.FromError(errors);
                }

                return ApiOperationResult<User>.FromSuccess(this._mapper.Map<User>(dbUser));
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.SignUpUnexpectedError, ex, ex.Message);
                return ApiOperationResult<User>.FromError(new SerializableError { { "UnexpectedError", "UnexpectedError" } });
            }
        }

        /// <inheritdoc/>
        public async Task<ApiOperationResult<TokenResponse>> LoginAsync(LoginUserRequest loginUserRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(
                        new Dictionary<string, string>
                        {
                        { "client_id", this._options.ClientId },
                        { "client_secret", this._options.ClientSecret },
                        { "grant_type", "password" },
                        { "scope", ApiConstant.Scopes },
                        { "username", loginUserRequest.Email },
                        { "password", loginUserRequest.Password },
                        }),
                    RequestUri = new Uri(new Uri(this._options.IdentityServerUrl), ApiConstant.GetTokenPath),
                };

                var response = await this._httpClient.SendAsync(requestMessage);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = new SerializableError { { "AUTH_ERROR", "AUTH_ERROR" } };
                    
                    this._logger.LogError(EventIds.IdentityServerInvalidStatusCodeError, exception: null, response.StatusCode.ToString(), responseContent);
                    
                    return ApiOperationResult<TokenResponse>.FromError(error);
                }

                return ApiOperationResult<TokenResponse>.FromSuccess(JsonSerializer.Deserialize<TokenResponse>(responseContent));
            }
            catch (Exception ex)
            {
                this._logger.LogError(EventIds.LoginUnexpectedError, ex, ex.Message);
                return ApiOperationResult<TokenResponse>.FromError(new SerializableError { { "UnexpectedError", "UnexpectedError" } });
            }
        }
    }
}

