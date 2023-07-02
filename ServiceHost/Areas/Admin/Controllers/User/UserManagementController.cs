using AccountManagement.Application.Contrast.Role;
using AccountManagement.Application.Contrast.User;
using AccountManagement.Domain.RoleAgg;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application;
using NewsManagement.Application.Contrasts.NewsCategory;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Admin.Controllers.User
{
	public class UserManagementController : Controller
    {
        private readonly IUserApplication _userApplication;
        private readonly IRoleApplication _roleApplication;

        public UserManagementController(IUserApplication userApplication, IRoleApplication roleApplication)
        {
            _userApplication = userApplication;
            _roleApplication = roleApplication;
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


    }
}
