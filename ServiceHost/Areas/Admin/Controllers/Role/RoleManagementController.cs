using AccountManagement.Application.Contrast.Role;
using BookShop.Areas.Admin.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Application.Contrasts.NewsCategory;
using Newtonsoft.Json;
using System.ComponentModel;

namespace ServiceHost.Areas.Admin.Controllers.Role
{
    //[Authorize(Policy = "Administration")]
    [DisplayName("مدیریت اخبار")]
    [Area("Admin")]
    public class RoleManagementController : Controller
    {
        private readonly IRoleApplication _roleApplication;

        public RoleManagementController(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        [Area("admin")]
        [Route("admin/role/index")]
        [HttpGet, DisplayName("مشاهده")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Index()
        {
            return View();
        }

        [Area("admin")]
        [Route("admin/role/getroles")]
        [HttpGet]

        public JsonResult GetRoles(string search, string order, int offset, int limit, string sort)
        {
            var searchModel = new RoleSearchModel
            {
                Limit = limit,
                Sort = sort,
                Offset = offset,
                Order = order,
                Search = search
            };

            var (roles, total) = _roleApplication.Search(searchModel);

            return Json(new { total = total, rows = roles });
        }


        [Area("admin")]
        [Route("admin/role/Create")]
        [HttpGet, DisplayName("ایجاد")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Create()
        {
            

            return PartialView("_Create",new CreateRole());
        }


  //      [Area("admin")]
  //      [Route("admin/role/Create")]
  //      [HttpPost]
  //      public void Create(CreateRole command)
  //      {

	        

	 //       var result = _roleApplication.Create(command);


	 //       TempData["ShowMassage"] = JsonConvert.SerializeObject(result);



		//}

        [Area("admin")]
        [Route("admin/role/Create")]
        [HttpPost]
        public IActionResult Create(CreateRole command)
        {

	        if (ModelState.IsValid)
	        {
		        var result = _roleApplication.Create(command);


		        TempData["ShowMassage"] = JsonConvert.SerializeObject(result);

		        
	        }
	       
	        return PartialView("_Create", command);
			


	        


        }


		[Area("admin")]
        [Route("admin/role/Edit")]
        [HttpGet, DisplayName("ویرایش")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Edit(long id)
        {
            var newRole = _roleApplication.GetAllRoles()
                .Select(p => new EditRole()
                {
                    Description = p.Description,
                    Id = p.Id,
                    Name = p.Name

                }).FirstOrDefault(p => p.Id == id);
            
            
            return PartialView("_Edit", newRole);
            
        }


        [Area("admin")]
        [Route("admin/role/Edit")]
        [HttpPost]
        public IActionResult Edit(EditRole command)
        {
	        if (ModelState.IsValid)
	        {
		        var result = _roleApplication.Edit(command);


		        TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
			}

	        return PartialView("_Edit", command);
        }

        [Area("admin")]
        [Route("admin/role/Delete")]
        [HttpGet, DisplayName("حذف")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Delete(long id)
        {
            var delRole = _roleApplication.GetAllRoles()
                .Select(p => new EditRole()
                {
                    Description = p.Description,
                    Id = p.Id,
                    Name = p.Name

                }).FirstOrDefault(p => p.Id == id);


            return PartialView("_Delete", delRole);

        }

        [Area("admin")]
        [Route("admin/role/Delete")]
        [HttpPost]
        public void Delete(EditRole commend)
        {
            var result = _roleApplication.Delete(commend);


            TempData["ShowMassage"] = JsonConvert.SerializeObject(result);

        }
    }
}
