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
using _1_NewsManagementQuery.Contracts.UserBookMark;
using NewsManagement.Application.Contrasts.BookMark;
using NewsManagement.Application.Contrasts.Like;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Application.Contrasts.Video;
using NewsManagement.Domain.VideoAgg;
using Microsoft.AspNetCore.Authorization;

namespace ServiceHost.Controllers
{
	public class HomeController : Controller
    {
        private readonly IHomePageQuery _homePageQuery;
        private readonly INewsDetailQuery _newsDetailQuery;
        private readonly INewsPaginateQuery _newsPaginateQuery;
        private readonly ICategoryOrTagInfoQuery _categoryOrTagInfoQuery;
        private readonly INewsInCategoriesAndTagsQuery _newsInCategoriesAndTagsQuery;
        private readonly IUserBookMarkQuery _userBookMarkQuery;
        private readonly IVisitApplication _visitApplication;
        private readonly IUserApplication _userApplication;
        private readonly IVideoApplication _videoApplication;
        private readonly IBookMarkApplication _bookMarkApplication;
        private readonly ILikeApplication _likeApplication;
        private readonly INewsApplication _newsApplication;



        public HomeController(IHomePageQuery homePageQuery, INewsDetailQuery newsDetailQuery, IVisitApplication visitApplication, IUserApplication userApplication, INewsPaginateQuery newsPaginateQuery, ICategoryOrTagInfoQuery categoryOrTagInfoQuery, INewsInCategoriesAndTagsQuery newsInCategoriesAndTagsQuery, IVideoApplication videoApplication, IUserBookMarkQuery userBookMarkQuery, IBookMarkApplication bookMarkApplication, ILikeApplication likeApplication, INewsApplication newsApplication)
        {
            _homePageQuery = homePageQuery;
            _newsDetailQuery = newsDetailQuery;
            _visitApplication = visitApplication;
            _userApplication = userApplication;
            _newsPaginateQuery = newsPaginateQuery;
            _categoryOrTagInfoQuery = categoryOrTagInfoQuery;
            _newsInCategoriesAndTagsQuery = newsInCategoriesAndTagsQuery;
            _videoApplication = videoApplication;
            _userBookMarkQuery = userBookMarkQuery;
            _bookMarkApplication = bookMarkApplication;
            _likeApplication = likeApplication;
            _newsApplication = newsApplication;
        }

        [Route("home/index")]
        [Route("/")]
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
        public IActionResult GetNewsInCategoryAndTag(int pageIndex, int pageSize, int categoryId, long tagId)
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

        [Route("Video")]
        public IActionResult Videos()
        {
            var video = _videoApplication.GetAllVideos();
            return View(video);
        }


        [Route("Video/{videoId}")]
        public IActionResult VideoDetails(long videoId)
        {
            if (videoId == 0)
                return NotFound();
            else
            {
                var video = _videoApplication.GetVideo(videoId);
                if (video == null)
                    return NotFound();
                else
                    return View(video);
            }
        }
        [Authorize(Policy = "WebsiteUser")]
        [Route("/home/profile")]
        public IActionResult Profile()
        {
            var userId = _userApplication.GetUser(User).Id;
            var newsBookMarked = _userBookMarkQuery.GetBookMark(userId);

            return View(newsBookMarked);
        }
        [Authorize(Policy = "WebsiteUser")]
        [HttpGet]
        public IActionResult DeleteBookmark(long newsId)
        {
	        var userId = _userApplication.GetUser(User).Id;
	        var bookMark = _bookMarkApplication.GetBookMark(newsId, userId);

	        return PartialView("_DeleteBookmark",bookMark);
        }
        [Authorize(Policy = "WebsiteUser")]
        [HttpPost]
        public IActionResult DeleteBookmark(DeleteBookMark command)
        {
	        var result = _bookMarkApplication.Delete(command);
	        if (result.IsSucceeded)
	        {
		        var newsBookMarked = _userBookMarkQuery.GetBookMark(command.UserId);
				return PartialView("_Bookmarks",newsBookMarked.Bookmarks );
	        }

	        return PartialView("_DeleteBookmark");
        }
        [HttpGet]
        public JsonResult LikeOrDisLike(long newsId, bool isLike)
        {
            _likeApplication.LikeOrDisLike(newsId,isLike);
            var likeAndDislike = _newsApplication.NumberOfLikeAndDisLike(newsId);

			return Json(new { like = likeAndDislike.NumberOfLike, dislike = likeAndDislike.NumberOfDisLike });
        }
        [Authorize(Policy = "WebsiteUser")]
        public JsonResult BookmarkNews(long newsId)
        {
	        var userId = _userApplication.GetUser(User).Id;
			var result =_bookMarkApplication.BookMarkNews(newsId, userId);
			if (result)
				return Json(true);
			else
			{
				return Json(false);
			}


        }
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}