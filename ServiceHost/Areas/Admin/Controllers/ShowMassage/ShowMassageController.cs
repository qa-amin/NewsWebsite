using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Admin.Controllers.ShowMassage
{
	public class ShowMassageController : Controller
	{
		[Area("admin")]
		[Route("admin/ShowMassage/index")]
		public JsonResult Index()
        {
            var value = JsonConvert.DeserializeObject<OperationResult>(TempData["ShowMassage"].ToString());
            

            return new JsonResult(value);
		}
	}
}
