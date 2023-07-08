using Microsoft.AspNetCore.Mvc;
using ServiceHost.Models;
using System.Diagnostics;
using _1_NewsManagementQuery.Contracts.HomePage;

namespace ServiceHost.Controllers
{
	public class HomeController : Controller
    {
        private readonly IHomePageQuery _homePageQuery;

        public HomeController(IHomePageQuery homePageQuery)
        {
            _homePageQuery = homePageQuery;
        }

        [Route("home/index")]
		public IActionResult Index()
        {
            var homePageQueryModel = _homePageQuery.GetNews();
			return View(homePageQueryModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}