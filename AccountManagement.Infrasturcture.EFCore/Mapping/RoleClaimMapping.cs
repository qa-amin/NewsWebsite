using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagement.Domain.RoleClaimAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
	public class RoleClaimMapping : IEntityTypeConfiguration<RoleClaim>
	{
		public void Configure(EntityTypeBuilder<RoleClaim> builder)
		{
			builder.ToTable("RoleClaim", "identity");

			builder.HasOne(p => p.Role)
				.WithMany(p => p.Claims)
				.HasForeignKey(p => p.RoleId);
		}
	}
}
