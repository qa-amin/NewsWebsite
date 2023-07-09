using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsManagement.Domain.LikeAgg;

namespace NewsManagement.Infrastructure.EFCore.Mapping
{
	public class LikeMapping : IEntityTypeConfiguration<Like>
	{
		public void Configure(EntityTypeBuilder<Like> builder)
		{
			builder.HasKey(p => new { p.IpAddress, p.NewsId });

			builder.HasOne(p => p.News)
				.WithMany(p => p.Likes)
				.HasForeignKey(p => p.NewsId);
		}
	}
}
