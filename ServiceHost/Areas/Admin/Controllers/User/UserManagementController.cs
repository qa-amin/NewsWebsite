using AccountManagement.Application.Contrast.Role;
using AccountManagement.Application.Contrast.User;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.UserAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application;
using NewsManagement.Application.Contrasts.NewsCategory;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Admin.Controllers.User
{
    [Authorize(Policy = "Administration")]
    public class UserManagementController : Controller
    {
        private readonly IUserApplication _userApplication;
        private readonly IRoleApplication _roleApplication;
        private readonly UserManager<AccountManagement.Domain.UserAgg.User> _userManager;

        public UserManagementController(IUserApplication userApplication, IRoleApplication roleApplication, UserManager<AccountManagement.Domain.UserAgg.User> userManager)
        {
            _userApplication = userApplication;
            _roleApplication = roleApplication;
            _userManager = userManager;
        }

        [Area("admin")]
        [Route("admin/user/index")]
        [HttpGet]
        public IActionResult Index()
		{
			return View();
		}

        [Area("admin")]
        [Route("admin/user/GetUsers")]
        [HttpGet]
        public JsonResult GetUsers(string search, string order, int offset, int limit, string sort)
        {
            var searchModel = new UserSearchModel()
            {
                Limit = limit,
                Sort = sort,
                Offset = offset,
                Order = order,
                Search = search
            };
            var (categories, total) = _userApplication.Search(searchModel);

            return Json(new { total = total, rows = categories });

           
        }



        [Area("admin")]
        [Route("admin/user/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = _roleApplication.GetAllRoles();

            return PartialView("_Create");
        }


        [Area("admin")]
        [Route("admin/user/Create")]
        [HttpPost]
        public IActionResult Create(CreateUser command)
        {

	        if (ModelState.IsValid)
	        {
		        var result = _userApplication.Create(command);
		        TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
			}

	        ViewBag.Roles = _roleApplication.GetAllRoles();

			return PartialView("_Create", command);


        }



        [Area("admin")]
        [Route("admin/user/Edit")]
        [HttpGet]
        public IActionResult Edit(long id)
        {
            ViewBag.Roles = _roleApplication.GetAllRoles();
            var user = _userApplication.GetUserWithRole(id);

            return PartialView("_Edit", user);
        }


        [Area("admin")]
        [Route("admin/user/Edit")]
        [HttpPost]
        public IActionResult Edit(EditUser command)
        {
	        ModelState.Remove("Password");
	        ModelState.Remove("ConfirmPassword");
	        ModelState.Remove("ImageFile");
	        ModelState.Remove("Image");
	        ModelState.Remove("Roles");

			if (ModelState.IsValid)
	        {
		        var result = _userApplication.Edit(command);

		        TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
			}


	        ViewBag.Roles = _roleApplication.GetAllRoles();
	        return PartialView("_Edit", command);



        }






        [Area("admin")]
        [Route("admin/user/Delete")]
        [HttpGet]
        public IActionResult Delete(long id)
        {
            
            var user = _userApplication.GetDetail(id);

            return PartialView("_Delete", user);
        }


        [Area("admin")]
        [Route("admin/user/Delete")]
        [HttpPost]
        public void Delete(EditUser command)
        {

            var result = _userApplication.Delete(command.Id);



            TempData["ShowMassage"] = JsonConvert.SerializeObject(result);



        }
        [Area("admin")]
        [Route("admin/user/Details")]
        public IActionResult Details(long Id)
        {
            var user = _userApplication.GetUserInfo(Id);
	        return View(user);
        }


        [Area("Admin")]
        [Route("admin/user/ResetPass")]
        [HttpGet]
        public IActionResult ResetPass(long userId)
        {


            return View(new ResetPass()
            {
                UserId = userId
            });
        }

        [Area("Admin")]
        [Route("admin/user/ResetPass")]
        [HttpPost]
        public IActionResult ResetPass(ResetPass command)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(command.UserId.ToString()).Result;

                var result = _userApplication.ResetPass(command, user);
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
        [Route("admin/user/ChangeEmailConfirmed")]
        [HttpGet]
        public IActionResult ChangeEmailConfirmed(long id)
        {
            var user =  _userManager.FindByIdAsync(id.ToString()).Result;
            string ResultJsonData;
            if (user == null)
            {
                return NotFound();
            }

            if (user.EmailConfirmed)
            {
                ResultJsonData = "تایید نشده";
                user.EmailConfirmed = false;
            }

            else
            {
                user.EmailConfirmed = true;
                ResultJsonData = "تایید شده";
            }

            var result =  _userManager.UpdateAsync(user).Result;
            return Json(ResultJsonData);
        }
        [Area("Admin")]
        [Route("admin/user/ChangeTwoFactorEnabled")]
        [HttpGet]
        public IActionResult ChangeTwoFactorEnabled(long id)
        {
            var user =  _userManager.FindByIdAsync(id.ToString()).Result;
            string ResultJsonData;
            if (user == null)
            {
                return NotFound();
            }

            if (user.TwoFactorEnabled)
            {
                user.TwoFactorEnabled = false;
                ResultJsonData = "غیرفعال";
            }

            else
            {
                user.TwoFactorEnabled = true;
                ResultJsonData = "فعال";
            }

            var result = _userManager.UpdateAsync(user).Result;
            return Json(ResultJsonData);
        }

        [Area("Admin")]
        [Route("admin/user/ChangeLockOutEnable")]
        [HttpGet]

        public IActionResult ChangeLockOutEnable(long id)
        {
            var user =  _userManager.FindByIdAsync(id.ToString()).Result;
            string ResultJsonData;
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if (user.LockoutEnabled)
                {
                    user.LockoutEnabled = false;
                    ResultJsonData = "غیرفعال";
                }

                else
                {
                    user.LockoutEnabled = true;
                    ResultJsonData = "فعال";
                }

                var result =  _userManager.UpdateAsync(user);
                return Json(ResultJsonData);
            }
        }

        [Area("Admin")]
        [Route("admin/user/InActiveOrActiveUser")]
        [HttpGet]
        public  IActionResult InActiveOrActiveUser(long id)
        {
            var user =  _userManager.FindByIdAsync(id.ToString()).Result;
            string ResultJsonData;
            if (user == null)
            {
                return NotFound();
            }

            if (user.IsActive)
            {
                user.DeActive();
                ResultJsonData = "غیرفعال";
            }

            else
            {
                user.Active();
                ResultJsonData = "فعال";
            }

             var result = _userManager.UpdateAsync(user).Result;
            return Json(ResultJsonData);
        }
        [Area("Admin")]
        [Route("admin/user/ChangePhoneNumberConfirmed")]
        [HttpGet]
        public  IActionResult ChangePhoneNumberConfirmed(long id)
        {
            var user =  _userManager.FindByIdAsync(id.ToString()).Result;
            string ResultJsonData;
            if (user == null)
            {
                return NotFound();
            }

            if (user.PhoneNumberConfirmed)
            {
                ResultJsonData = "تایید نشده";
                user.PhoneNumberConfirmed = false;
            }

            else
            {
                ResultJsonData = "تایید شده";
                user.PhoneNumberConfirmed = true;
            }

            var result =  _userManager.UpdateAsync(user).Result;
            return Json(ResultJsonData);
        }

        [Area("Admin")]
        [Route("admin/user/LockOrUnLockUserAccount")]
        [HttpGet]
        public IActionResult LockOrUnLockUserAccount(long Id)
        {
            var user =  _userManager.FindByIdAsync(Id.ToString()).Result;
            string ResultJsonData;
            if (user == null)
            {
                return NotFound();
            }

            if (user.LockoutEnd == null)
            {
                ResultJsonData = "قفل شده";
                user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(20);
            }

            else
            {
                if (user.LockoutEnd > DateTime.Now)
                {
                    ResultJsonData = "قفل نشده";
                    user.LockoutEnd = null;
                }
                else
                {
                    ResultJsonData = "قفل شده";
                    user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(20);
                }
            }

            var result =  _userManager.UpdateAsync(user).Result;
            return Json(ResultJsonData);
        }
    }
}
