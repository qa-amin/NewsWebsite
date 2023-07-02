using System.Globalization;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using AccountManagement.Application;
using AccountManagement.Application.Contrast.Role;
using AccountManagement.Application.Contrast.User;
using AccountManagement.Domain.UserAgg;

namespace AccountManagement.Infrastructure.Configuration
{
	public class AccountManagementBootstrapper
	{
		public static void Config(IServiceCollection services, string cs)
		{
			services.AddDbContext<AccountManagementDbContext>(x => x.UseSqlServer(cs));

            services.AddTransient<IUserApplication, UserApplication>();
            services.AddTransient<IRoleApplication, RoleApplication>();

			services.Configure<IdentityOptions>(options =>
			{
				options.User.RequireUniqueEmail = true;

				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 3;


			});
		}
	}
}