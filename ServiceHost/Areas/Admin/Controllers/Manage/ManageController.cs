using AccountManagement.Application.Contrast.User;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Admin.Controllers.Manage
{
	public class ManageController : Controller
    {
        private readonly IUserApplication _userApplication;

        public ManageController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [Area("admin")]
        [Route("admin/manage/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [Area("admin")]
        [Route("admin/manage/Login")]
        [HttpPost]
        public IActionResult Login(Login command)
        {
            if (ModelState.IsValid)
            {
               var result =  _userApplication.Login(command);
               if (result.IsSucceeded)
               {
                   return RedirectToAction("index", "Dashboard");
               }
               ModelState.AddModelError(string.Empty,result.Message);
            }
            return View(command);
        }

        [Area("Admin")]
        [Route("admin/manage/SignOut")]
        public IActionResult SignOut()
        {
            _userApplication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
