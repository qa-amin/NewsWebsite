using Microsoft.AspNetCore.Mvc;
using ServiceHost.Models;
using System.Diagnostics;
using _1_NewsManagementQuery.Contracts.HomePage;
using _1_NewsManagementQuery.Contracts.NewsDetail;
using AccountManagement.Application.Contrast.User;
using NewsManagement.Application.Contrasts.Visit;

namespace ServiceHost.Controllers
{
	public class HomeController : Controller
    {
        private readonly IHomePageQuery _homePageQuery;
        private readonly INewsDetailQuery _newsDetailQuery;
        private readonly IVisitApplication _visitApplication;
        private readonly IUserApplication _userApplication;
         

        public HomeController(IHomePageQuery homePageQuery, INewsDetailQuery newsDetailQuery, IVisitApplication visitApplication, IUserApplication userApplication)
        {
            _homePageQuery = homePageQuery;
            _newsDetailQuery = newsDetailQuery;
            _visitApplication = visitApplication;
            _userApplication = userApplication;
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

        [Route("News/{newsId}/{url}")]

        public IActionResult NewsDetails(long newsId, string url)
        {
            _visitApplication.VisitLog(newsId);

            long? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = _userApplication.GetUser(User).Id;
            }

            var getDetails = _newsDetailQuery.GetDetail(newsId, userId);

            return View(getDetails);

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}