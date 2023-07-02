using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsWebsite.Entities;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Admin.Controllers.Category
{
	public class CategoryController : Controller
	{
		private readonly INewsCategoryApplication _newsCategoryApplication;

		public CategoryController(INewsCategoryApplication newsCategoryApplication)
		{
			_newsCategoryApplication = newsCategoryApplication;
		}

		[Area("admin")]
		[Route("admin/category/index")]
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[Area("admin")]
		[Route("admin/category/GetNewsCategory")]
		[HttpGet]
		public IActionResult GetNewsCategory(string search, string order, int offset, int limit, string sort)
		{
			var searchModel = new NewsCategorySearchModel
			{
				Limit = limit,
				Sort = sort,
				Offset = offset,
				Order = order,
				Search = search
			};
			var (categories, total) = _newsCategoryApplication.Search(searchModel);
			
			return Json(new { total = total, rows = categories });
		}


		[Area("admin")]
		[Route("admin/category/Create")]
		[HttpGet]
		public IActionResult Create()
        {
            ViewBag.Categories = _newsCategoryApplication.GetAllCategories();

            return PartialView("_Create");
		}


        [Area("admin")]
        [Route("admin/category/Create")]
        [HttpPost]
        public IActionResult Create(CreateNewsCategory command)
        {
	        if (ModelState.IsValid)
	        {
		        var result = _newsCategoryApplication.Create(command);


		        TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
			}

	        return PartialView("_Create");

        }


        [Area("admin")]
        [Route("admin/category/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _newsCategoryApplication.GetAllCategories();
            var newsCategory = _newsCategoryApplication.Getdetails(id);
            return PartialView("_Edit",newsCategory);
        }


        [Area("admin")]
        [Route("admin/category/Edit")]
        [HttpPost]
        public IActionResult Edit(EditNewsCategory command)
        {
	        if (ModelState.IsValid)
	        {
		        var result = _newsCategoryApplication.Edit(command);

		        TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
			}

	        ViewBag.Categories = _newsCategoryApplication.GetAllCategories();
	        return PartialView("_Edit", command);
        }


        [Area("admin")]
        [Route("admin/category/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var newsCategory = _newsCategoryApplication.Getdetails(id);
            return PartialView("_Delete", newsCategory);
        }


        [Area("admin")]
        [Route("admin/category/Delete")]
        [HttpPost]
        public void Delete(EditNewsCategory command)
        {
            var result = _newsCategoryApplication.Delete(command);

            TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
        }
    }
}
