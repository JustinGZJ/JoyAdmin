using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoyAdmin.Web.Entry.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.Description = "JoyAdmin | 规范化接口";

            return View();
        }
    }
}