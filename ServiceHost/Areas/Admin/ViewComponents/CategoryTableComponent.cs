using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Admin.ViewComponents
{
	public class CategoryTableComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
