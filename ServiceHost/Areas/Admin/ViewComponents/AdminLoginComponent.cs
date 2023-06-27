using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Admin.ViewComponents
{
	public class AdminLoginComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
