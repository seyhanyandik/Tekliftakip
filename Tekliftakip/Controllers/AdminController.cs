using Microsoft.AspNetCore.Mvc;

namespace Tekliftakip.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
