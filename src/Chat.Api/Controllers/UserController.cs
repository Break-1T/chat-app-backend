using Chat.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// UserController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <exception cref="System.ArgumentNullException">userService</exception>
        public UserController(IUserService userService)
        {
            this._userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
    }
}
