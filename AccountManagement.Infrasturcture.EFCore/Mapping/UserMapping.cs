using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagement.Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
	public class UserMapping : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users", "identity");
			builder.HasKey(x => x.Id);


			builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
			builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
		}
	}
}
