using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.NewsLetter;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Admin.Controllers.NewsLetter
{
    [Authorize(Policy = "Administration")]
    public class NewsLetterController : Controller
	{
		private readonly INewsLetterApplication _newsLetterApplication;

		public NewsLetterController(INewsLetterApplication newsLetterApplication)
		{
			_newsLetterApplication = newsLetterApplication;
		}

		[Area("Admin")]
		[Route("admin/newsletter/index")]
		public IActionResult Index()
		{
			return View();
		}
		[Area("Admin")]
		[Route("admin/newsletter/GetNewsletter")]
		public JsonResult GetNewsletter(string search, string order, int offset, int limit, string sort)
		{
			var searchModel = new NewsLetterSearchModel
			{
				Limit = limit,
				Sort = sort,
				Order = order,
				Offset = offset,
				Search = search
			};

			var (newsLetter, total) = _newsLetterApplication.Search(searchModel);


			return Json(new {total =total, rows = newsLetter });
		}

		[Area("Admin")]
		[Route("admin/newsletter/Create")]
		[HttpPost]
		public IActionResult Create(CreateNewsLetter command)
		{
			if (ModelState.IsValid)
			{
				var result = _newsLetterApplication.Create(command);
				if (result.IsSucceeded)
				{
					TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
				}
				else
				{
					ModelState.AddModelError(string.Empty, result.Message);
				}
			}

			return PartialView("_RegisterInNewsletter",command);
		}

		[Area("Admin")]
		[Route("admin/newsletter/Delete")]
		[HttpGet]
		public IActionResult Delete(string email)
		{

			var newsLetter = _newsLetterApplication.GetDetails(email);
			

			return PartialView("_Delete", newsLetter);
		}


		[Area("Admin")]
		[Route("admin/newsletter/Delete")]
		[HttpPost]
		public void Delete(EditNewsLetter command)
		{
			
				var result = _newsLetterApplication.Delete(command);
				
				TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
				
			

			
		}

		[Area("Admin")]
		[Route("admin/newsletter/ActiveOrInActiveMembers")]
		[HttpPost]
		public IActionResult ActiveOrInActiveMembers(string email)
		{

			var result = _newsLetterApplication.ActiveOrInActiveMembers(email);

				return Json(result.Message);
			


		}
	}
}
