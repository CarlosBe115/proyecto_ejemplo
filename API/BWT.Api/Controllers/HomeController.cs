using Microsoft.AspNetCore.Mvc;

namespace BWT.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
