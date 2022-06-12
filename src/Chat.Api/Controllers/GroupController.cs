using Chat.Api.Extensions;
using Chat.Api.Interfaces;
using Chat.Api.Models.Groups;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// GroupController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupController"/> class.
        /// </summary>
        /// <param name="groupService">The group service.</param>
        /// <exception cref="System.ArgumentNullException">groupService</exception>
        public GroupController(IGroupService groupService)
        {
            this._groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        /// <summary>
        /// Creates the specified create group request.
        /// </summary>
        /// <param name="createGroupRequest">The create group request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(Group), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(SerializableError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] Group createGroupRequest, CancellationToken cancellationToken = default)
        {
            var currentUserId = this.User.GetUserId();
            var createGroupResult = await this._groupService.CreateGroupAsync(createGroupRequest, currentUserId, cancellationToken);

            if (!createGroupResult.IsSuccess)
            {
                return this.BadRequest(createGroupResult.Error);
            }

            return this.Created(this.Request.Path, createGroupResult.Entity);
        }
    }
}
