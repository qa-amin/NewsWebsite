using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Admin.Controllers.AccessDenied
{
    public class AccessDeniedController : Controller
    {
        [Area("Admin")]
        [Route("admin/AccessDenied")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
