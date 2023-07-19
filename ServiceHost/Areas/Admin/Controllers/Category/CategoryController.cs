using _0_Framework.Application;
using BookShop.Areas.Admin.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Web.Mvc;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsWebsite.Entities;
using Newtonsoft.Json;
using System.ComponentModel;

namespace ServiceHost.Areas.Admin.Controllers.Category
{
    [DisplayName("مدیریت دسته بندی ها")]
    [Area("Admin")]
    public class CategoryController : Controller
	{
		private readonly INewsCategoryApplication _newsCategoryApplication;

		public CategoryController(INewsCategoryApplication newsCategoryApplication)
		{
			_newsCategoryApplication = newsCategoryApplication;
		}

		[Area("Admin")]
		[Route("admin/category/index")]
        [HttpGet, DisplayName("مشاهده")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Index()
		{
			return View();
		}

        [Area("Admin")]
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


        [Area("Admin")]
        [Route("admin/category/Create")]
        [HttpGet, AjaxOnly, DisplayName("درج")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Create()
        {
            ViewBag.Categories = _newsCategoryApplication.GetAllCategories();

            return PartialView("_Create");
		}


        [Area("Admin")]
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


        [Area("Admin")]
        [Route("admin/category/Edit")]
        [HttpGet, AjaxOnly, DisplayName("ویرایش")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _newsCategoryApplication.GetAllCategories();
            var newsCategory = _newsCategoryApplication.Getdetails(id);
            return PartialView("_Edit",newsCategory);
        }


        [Area("Admin")]
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


        [Area("Admin")]
        [Route("admin/category/Delete")]
        [HttpGet, AjaxOnly, DisplayName("حذف")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Delete(int id)
        {
            var newsCategory = _newsCategoryApplication.Getdetails(id);
            return PartialView("_Delete", newsCategory);
        }


        [Area("Admin")]
        [Route("admin/category/Delete")]
        [HttpPost]
        public void Delete(EditNewsCategory command)
        {
            var result = _newsCategoryApplication.Delete(command);

            TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
        }
    }
}
