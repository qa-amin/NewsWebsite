using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.NewsCategory;

namespace ServiceHost.Areas.Admin.Controllers
{
	public class HomeController : Controller
	{
		private readonly INewsCategoryApplication _newsCategoryApplication;

		public HomeController(INewsCategoryApplication newsCategoryApplication)
		{
			_newsCategoryApplication = newsCategoryApplication;
		}

		[Area("Admin")]
		[Route("admin/home/index")]
		public IActionResult Index()
		{
			
			return View();
		}
	}
}
