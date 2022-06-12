using Chat.Api.Interfaces;
using Chat.Db.Models;
using Microsoft.AspNetCore.Identity;

namespace Chat.Api.Services
{
    /// <summary>
    /// UserService.
    /// </summary>
    /// <seealso cref="Chat.Api.Interfaces.IUserService" />
    public class UserService : IUserService
    {
        private readonly UserManager<AppIdentityUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <exception cref="System.ArgumentNullException">userManager</exception>
        public UserService(UserManager<AppIdentityUser> userManager)
        {
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
    }
}
