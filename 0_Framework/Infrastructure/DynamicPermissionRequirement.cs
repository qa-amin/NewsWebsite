using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using _0_Framework.Application;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Features;

namespace NewsWebsite.IocConfig
{
    public class DynamicPermissionRequirement : IAuthorizationRequirement
    {
    }

    public class DynamicPermissionsAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
    {
        private readonly ISecurityTrimmingService _securityTrimmingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DynamicPermissionsAuthorizationHandler(ISecurityTrimmingService securityTrimmingService, IHttpContextAccessor httpContextAccessor)
        {
            _securityTrimmingService = securityTrimmingService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
             AuthorizationHandlerContext context,
             DynamicPermissionRequirement requirement)
        {

            if (context.Resource is HttpContext httpContext)
            {
                var endpoint = httpContext.GetEndpoint();
                var actionDescriptor = endpoint.Metadata.OfType<ControllerActionDescriptor>().SingleOrDefault();

                actionDescriptor.RouteValues.TryGetValue("area", out var areaName);
                var area = string.IsNullOrWhiteSpace(areaName) ? string.Empty : areaName;

                actionDescriptor.RouteValues.TryGetValue("controller", out var controllerName);
                var controller = string.IsNullOrWhiteSpace(controllerName) ? string.Empty : controllerName;

                actionDescriptor.RouteValues.TryGetValue("action", out var actionName);
                var action = string.IsNullOrWhiteSpace(actionName) ? string.Empty : actionName;

                if (_securityTrimmingService.CanCurrentUserAccess(area, controller, action))
                {
                    context.Succeed(requirement);
                }

                return Task.CompletedTask;
            }
               
            

            return Task.CompletedTask;

        }
    }
}
