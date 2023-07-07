using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsManagement.Application.Contrasts.Tag;
using NewsManagement.Application.Contrasts.Video;
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
        [Area("Admin")]
        [Route("admin/news/Edit")]
        [HttpGet]
        public IActionResult Edit(long id)
        {
	        ViewBag.Tags = _tagApplication.GetAllTags().Select(p => p.TagName).ToList();
	        var news = _newsApplication.GetDetails(id);
	        news.NewsNewsCategoryViewModel =
		        new NewsNewsCategoryViewModel(_newsCategoryApplication.GetAllCategories(), _newsApplication.CategoryIds(news.Id));

            news.NameOfTags = _newsApplication.GetTagNames(news.Id);
			return View(news);
        }

        [Area("Admin")]
        [Route("admin/news/Edit")]
        [HttpPost]
        public IActionResult Edit(EditNews command)
        {
	        
	        ModelState.Remove("ImageFile");
	        ModelState.Remove("NewsNewsCategoryViewModel");
	        ModelState.Remove("PublishDateTime");
	        ModelState.Remove("PersianPublishTime");
	        ModelState.Remove("PersianPublishDate");

			if (ModelState.IsValid)
	        {
		        var result = _newsApplication.Edit(command);
		        if (result.IsSucceeded)
		        {
			       return RedirectToAction("Index", "News");
		        }
		        else
		        {
                    ModelState.AddModelError(string.Empty,result.Message);
		        }
	        }

	        ViewBag.Tags = _tagApplication.GetAllTags().Select(p => p.TagName).ToList();
	       command.NewsNewsCategoryViewModel= new NewsNewsCategoryViewModel(_newsCategoryApplication.GetAllCategories(), _newsApplication.CategoryIds(command.Id));
	       command.NameOfTags = _newsApplication.GetTagNames(command.Id);
			return View(command);

		}


		[Area("Admin")]
        [Route("admin/news/Delete")]
        [HttpGet]
        public IActionResult Delete(long id)
        {
            var video = _newsApplication.GetDetails(id);
            return PartialView("_Delete", video);
        }
        [Area("Admin")]
        [Route("admin/news/Delete")]
        [HttpPost]
        public void Delete(EditVideo command)
        {

            var result = _newsApplication.Delete(command.Id);
            TempData["ShowMassage"] = JsonConvert.SerializeObject(result);



        }


    }
}
