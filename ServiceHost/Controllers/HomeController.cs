using Microsoft.AspNetCore.Mvc;
using ServiceHost.Models;
using System.Diagnostics;
using _1_NewsManagementQuery.Contracts.CategoryOrTagInfo;
using _1_NewsManagementQuery.Contracts.HomePage;
using _1_NewsManagementQuery.Contracts.NewsDetail;
using _1_NewsManagementQuery.Contracts.NewsPaginate;
using AccountManagement.Application.Contrast.User;
using NewsManagement.Application.Contrasts.Visit;
using _0_Framework.Application;
using _1_NewsManagementQuery.Contracts.NewsInCategoriesAndTags;

namespace ServiceHost.Controllers
{
	public class HomeController : Controller
    {
        private readonly IHomePageQuery _homePageQuery;
        private readonly INewsDetailQuery _newsDetailQuery;
        private readonly INewsPaginateQuery _newsPaginateQuery;
        private readonly ICategoryOrTagInfoQuery _categoryOrTagInfoQuery;
        private readonly INewsInCategoriesAndTagsQuery _newsInCategoriesAndTagsQuery;
        private readonly IVisitApplication _visitApplication;
        private readonly IUserApplication _userApplication;


         

        public HomeController(IHomePageQuery homePageQuery, INewsDetailQuery newsDetailQuery, IVisitApplication visitApplication, IUserApplication userApplication, INewsPaginateQuery newsPaginateQuery, ICategoryOrTagInfoQuery categoryOrTagInfoQuery, INewsInCategoriesAndTagsQuery newsInCategoriesAndTagsQuery)
        {
            _homePageQuery = homePageQuery;
            _newsDetailQuery = newsDetailQuery;
            _visitApplication = visitApplication;
            _userApplication = userApplication;
            _newsPaginateQuery = newsPaginateQuery;
            _categoryOrTagInfoQuery = categoryOrTagInfoQuery;
            _newsInCategoriesAndTagsQuery = newsInCategoriesAndTagsQuery;
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

        public IActionResult GetNewsPaginate(int limit, int offset)
        {
            var newsPageinate = _newsPaginateQuery.GetNewsPaginate(limit, offset);

            return PartialView("_NewsPaginate", newsPageinate);
        }

        [Route("Category/{categoryId}/{url}")]
        public IActionResult NewsInCategory(int categoryId, string url)
        {
            var newsCategory = _categoryOrTagInfoQuery.FindCategory(categoryId);
            if (newsCategory == null)
            {
                return NotFound();
            }

            return View("NewsInCategoryAndTag", newsCategory);
        }
        [Route("Tag/{tagId}")]
        public IActionResult NewsInCategory(long tagId)
        {
            var tag = _categoryOrTagInfoQuery.FindTag(tagId);
            if (tag == null)
            {
                return NotFound();
            }

            return View("NewsInCategoryAndTag", tag);
        }

        [Route("home/GetNewsInCategoryAndTag")]
        [HttpGet]
        public async Task<ActionResult> GetNewsInCategoryAndTag(int pageIndex, int pageSize, int categoryId, long tagId)
        {

            if (categoryId != 0)
            {
                var newsInCategory = _newsInCategoriesAndTagsQuery.GetNewsInCategory(categoryId, pageIndex, pageSize);
                return Json(newsInCategory);
            }
            else
            {
                var newsInTag = _newsInCategoriesAndTagsQuery.GetNewsInTag(tagId, pageIndex, pageSize);
                return Json(newsInTag);
            }


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}