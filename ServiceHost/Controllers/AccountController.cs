using AccountManagement.Application.Contrast.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ServiceHost.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserApplication _userApplication;

        public AccountController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        
        [HttpGet]
        public IActionResult SignIn()
        {
            return PartialView("_SignIn");
        }
        
        [HttpPost]
        public IActionResult SignIn(Login command)
        {
            if (ModelState.IsValid)
            {
                var result = _userApplication.Login(command);
                if (result.IsSucceeded)
                {
                    return Json(result.Message);
                }
                
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return PartialView("_SignIn");
        }

        public  IActionResult Profile()
        {
            var userId = _userApplication.GetUser(User).Id;


            return RedirectToAction("Profile", "Manage", new { Area = "admin", id= userId });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return PartialView("_Register");
        }
        
        [HttpPost]
        public IActionResult Register(CreateUser command)
        {
            ModelState.Remove("ImageFile");
            ModelState.Remove("PersianBirthDate");
            ModelState.Remove("PhoneNumber");
            ModelState.Remove("RoleId");
            if (ModelState.IsValid)
            {
                var result = _userApplication.Register(command);
                TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
            }



            return PartialView("_Register");


        }
    }
}
