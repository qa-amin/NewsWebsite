using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsManagement.Domain.VisitAgg;

namespace NewsManagement.Infrastructure.EFCore.Mapping
{
	public class VisitMapping : IEntityTypeConfiguration<Visit>
	{
		public void Configure(EntityTypeBuilder<Visit> builder)
		{
			builder.HasKey(p => new { p.NewsId, p.IpAddress });

			builder.HasOne(p => p.News)
				.WithMany(p => p.Visits)
				.HasForeignKey(p => p.NewsId);
		}
	}
}
