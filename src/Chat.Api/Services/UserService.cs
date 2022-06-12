using AutoMapper;
using Chat.Api.Constants;
using Chat.Api.Interfaces;
using Chat.Api.Models.Users;
using Chat.Api.ResultModels;
using Chat.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <exception cref="System.ArgumentNullException">userManager</exception>
        public UserService(ILogger<UserService> logger, IMapper mapper, UserManager<AppIdentityUser> userManager)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <inheritdoc/>
        public async Task<ApiOperationResult<User>> CreateUserAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default)
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
                this._logger.LogError(EventIds.CreateUserUnexpectedError, ex, ex.Message);
                return ApiOperationResult<User>.FromError(new SerializableError { { "UnexpectedError" , "UnexpectedError" } });
            }
        }
    }
}

