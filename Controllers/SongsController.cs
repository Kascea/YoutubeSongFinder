using Microsoft.AspNetCore.Mvc;

namespace MontageWebsite.Controllers
{
    public class SongsController : Controller
    {
        public IActionResult Search()
        {
            return View();
        }
    }
}
