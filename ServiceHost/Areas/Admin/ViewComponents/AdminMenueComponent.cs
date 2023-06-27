using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Admin.ViewComponents
{
	public class AdminMenueComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
