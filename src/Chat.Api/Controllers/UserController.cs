using Chat.Api.Interfaces;
using Chat.Api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        /// <summary>
        /// Creates the specified create user request.
        /// </summary>
        /// <param name="createUserRequest">The create user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(SerializableError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest createUserRequest, CancellationToken cancellationToken = default)
        {
            var createUserResult = await this._userService.CreateUserAsync(createUserRequest, cancellationToken);

            if (!createUserResult.IsSuccess)
            {
                return this.BadRequest(createUserResult.Error);
            }

            return this.Created(this.Request.Path, createUserResult.Entity);
        }
    }
}
