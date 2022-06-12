using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;

namespace Chat.Api.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [Route("/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [Route("")]
        [Route("/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ExcludeFromCodeCoverage] // this for Assembly
        public IActionResult Index()
        {
            var info = $"{Assembly.GetEntryAssembly()?.GetName().Name}, {Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion}";

            return this.Ok(info);
        }

        /// <summary>
        /// Errors this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [Route("/Home/Error")]
        public IActionResult Error()
        {
            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            var message =
                $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.LogInformation(message);

            return new ObjectResult(new
            { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier, })
            { StatusCode = (int)HttpStatusCode.BadRequest };
        }
    }
}
