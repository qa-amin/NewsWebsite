using System.Web.Mvc;
using _0_Framework.Domain;
using AccountManagement.Application.Contrast.DynamicAccess;
using AccountManagement.Application.Contrast.User;
using AccountManagement.Domain.UserAgg;
using BookShop.Areas.Admin.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace ServiceHost.Areas.Admin.Controllers.DynamicAccess
{
    [Microsoft.AspNetCore.Authorization.Authorize(Policy = "Administration")]
    public class DynamicAccessController : Controller
    {
        private readonly IUserApplication _userApplication;
        private readonly IMvcActionsDiscoveryService _actionsDiscoveryService;

        public DynamicAccessController( IMvcActionsDiscoveryService actionsDiscoveryService, IUserApplication userApplication)
        {
            _actionsDiscoveryService = actionsDiscoveryService;
            _userApplication = userApplication;
        }

        [Area("admin")]
        [Microsoft.AspNetCore.Mvc.Route("admin/DynamicAccess/index")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Index(long userId)
        {
            if (userId == 0)
            {
                return NotFound();
            }

            var user = _userApplication.FindClaimsInUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            var securedControllerActions =
                _actionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);


            return View(new DynamicAccessIndexViewModel
            {
                UserIncludeUserClaims = user,
                SecuredControllerActions = securedControllerActions,
            });
        }

        [Area("admin")]
        [Microsoft.AspNetCore.Mvc.Route("admin/DynamicAccess/index")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Index(DynamicAccessIndexViewModel access)
        {
            var result =  _userApplication.AddOrUpdateClaimsAsync(access.UserId, ConstantPolicies.DynamicPermissionClaimType, access.ActionIds.Split(","));
            if (!result.Succeeded)
                ModelState.AddModelError(string.Empty, "در حین انجام عملیات خطایی رخ داده است.");

            return RedirectToAction("Index", new { userId = access.UserId });
        }
    }
}
