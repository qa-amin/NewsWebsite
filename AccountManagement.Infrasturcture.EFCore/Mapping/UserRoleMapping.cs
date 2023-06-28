using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagement.Domain.UserRoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
	public class UserRoleMapping : IEntityTypeConfiguration<UserRole>
	{
		
		public void Configure(EntityTypeBuilder<UserRole> builder)
		{
			builder.ToTable("UserRole", "identity");

			builder.HasOne(p => p.User)
				.WithMany(p => p.Roles)
				.HasForeignKey(p => p.UserId);

			builder.HasOne(p => p.Role)
				.WithMany(p => p.Users)
				.HasForeignKey(p => p.RoleId);
		}
	}
}
