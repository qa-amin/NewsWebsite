using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Admin.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        [Area("admin")]
        [Route("admin/dashboard/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
