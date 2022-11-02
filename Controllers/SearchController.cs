using Microsoft.AspNetCore.Mvc;

namespace MontageWebsite.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
