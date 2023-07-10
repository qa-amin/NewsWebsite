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
		public IActionResult Index(string? duration, string? TypeOfNews)
        {
	        var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax && TypeOfNews == "MostViewedNews")
            {
                return PartialView("_MostViewedNews", _homePageQuery.MostViewedNews(0, 3, duration));
            }
            else if (isAjax && TypeOfNews == "MostTalkNews")
			{
				return PartialView("_MostTalkNews", _homePageQuery.MostTalkNews(0, 3, duration));
			}
			else
	        {
				var homePageQueryModel = _homePageQuery.GetNews();
				return View(homePageQueryModel);
			}

			
		}

		

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}