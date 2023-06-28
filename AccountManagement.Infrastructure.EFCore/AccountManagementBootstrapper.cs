using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using AccountManagement.Domain.UserAgg;
namespace AccountManagement.Infrastructure.Configuration
{
	public class AccountManagementBootstrapper
	{
		public static void Config(IServiceCollection services, string cs)
		{
			services.AddDbContext<AccountManagementDbContext>(x => x.UseSqlServer(cs));

		}
	}
}