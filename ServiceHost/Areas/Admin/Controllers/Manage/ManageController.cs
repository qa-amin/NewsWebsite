using AccountManagement.Application.Contrast.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Admin.Controllers.Manage
{
	public class ManageController : Controller
    {
        private readonly IUserApplication _userApplication;
        private readonly UserManager<AccountManagement.Domain.UserAgg.User> _userManager;

        public ManageController(IUserApplication userApplication, UserManager<AccountManagement.Domain.UserAgg.User> userManager)
        {
            _userApplication = userApplication;
            _userManager = userManager;
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
            return Redirect("/");
        }

        [Area("Admin")]
        [Route("admin/manage/ChangePassword")]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            
            
            return View(new ChangePass());
        }

        [Area("Admin")]
        [Route("admin/manage/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePass command)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;

                var result = _userApplication.ChangePssword(command, user);
                if (result.IsSucceeded)
                {
                    ViewBag.Alert = result.Message;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }

               
            }

            return View(command);
        }

        [Area("Admin")]
        [Route("admin/manage/Profile")]
        [HttpGet]
        public IActionResult Profile(long id)
        {
            var user = _userApplication.GetProfileDetail(id);

            return View(user);
        }

        [Area("Admin")]
        [Route("admin/manage/Profile")]
        [HttpPost]
        public IActionResult Profile(ProfileViewModel command)
        {
            var image =command.Image;
            if (ModelState.IsValid)
            {
                var (result,Image) = _userApplication.EditProfile(command);
                image = Image;
                if (result.IsSucceeded)
                {
                    ViewBag.Alert = result.Message;
                }
                else
                {
                    ModelState.AddModelError(string.Empty,result.Message);
                }
            }

            command.Image = image;
            return View(command);
        }

    }
}
