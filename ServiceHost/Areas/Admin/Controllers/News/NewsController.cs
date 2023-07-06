using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsManagement.Application.Contrasts.Tag;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Admin.Controllers.News
{
    public class NewsController : Controller
    {
        private readonly INewsApplication _newsApplication;
        private readonly ITagApplication _tagApplication;
        private readonly INewsCategoryApplication _newsCategoryApplication;

        public NewsController( ITagApplication tagApplication, INewsCategoryApplication newsCategoryApplication, INewsApplication newsApplication)
        {
            
            _tagApplication = tagApplication;
            _newsCategoryApplication = newsCategoryApplication;
            _newsApplication = newsApplication;
        }

        [Area("Admin")]
        [Route("/admin/news/index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Area("Admin")]
        [Route("/admin/news/GetNews")]
        [HttpGet]
        public JsonResult GetNews(string search, string order, int offset, int limit, string sort)
        {
            var searchModel = new NewsSearchModel()
            {
                Limit = limit,
                Sort = sort,
                Offset = offset,
                Order = order,
                Search = search
            };

            var (News, total) = _newsApplication.Search(searchModel);
            return Json(new { total = total, rows = News });
            }

        [Area("Admin")]
        [Route("/admin/news/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Tags = _tagApplication.GetAllTags().Select(p => p.TagName).ToList();
            var createNews = new CreateNews();
            createNews.NewsNewsCategoryViewModel =
                new NewsNewsCategoryViewModel(_newsCategoryApplication.GetAllCategories(), null); 
            return View(createNews);
        }

        [Area("Admin")]
        [Route("/admin/news/Create")]
        [HttpPost]
        public IActionResult Create(CreateNews command)
        {
            
            ModelState.Remove("ImageName");
            ModelState.Remove("NewsNewsCategoryViewModel");
            ModelState.Remove("PublishDateTime");
            ModelState.Remove("PersianPublishTime");
            ModelState.Remove("PersianPublishDate");
            if (ModelState.IsValid)
            {
                var result = _newsApplication.Create(command);
                if (result.IsSucceeded)
                {
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }
            

            ViewBag.Tags = _tagApplication.GetAllTags().Select(p => p.TagName).ToList();
            var createNews = new CreateNews();
            createNews.NewsNewsCategoryViewModel = new NewsNewsCategoryViewModel(_newsCategoryApplication.GetAllCategories(), null);
            return View(createNews);


        }
    }
}
