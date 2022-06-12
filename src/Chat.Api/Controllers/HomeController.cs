using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
