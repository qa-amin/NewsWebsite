using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.RoleClaimAgg;
using AccountManagement.Domain.UserAgg;
using AccountManagement.Domain.UserClaimAgg;
using AccountManagement.Domain.UserRoleAgg;
using AccountManagement.Infrastructure.EFCore.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore
{
	public class AccountManagementDbContext : IdentityDbContext<User, Role, long, UserClaim, UserRole, IdentityUserLogin<long>, RoleClaim, IdentityUserToken<long>>
	{
		public AccountManagementDbContext(DbContextOptions<AccountManagementDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.HasDefaultSchema("identity");
			builder.ApplyConfiguration(new UserMapping());
			builder.ApplyConfiguration(new RoleMapping());
			builder.ApplyConfiguration(new UserRoleMapping());
			builder.ApplyConfiguration(new UserClaimMapping());
			builder.ApplyConfiguration(new RoleClaimMapping());
		}
	}
}
