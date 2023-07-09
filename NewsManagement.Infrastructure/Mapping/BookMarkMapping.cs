using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsManagement.Domain.BookMarkAgg;

namespace NewsManagement.Infrastructure.EFCore.Mapping
{
	public class BookMarkMapping : IEntityTypeConfiguration<BookMark>
	{
		public void Configure(EntityTypeBuilder<BookMark> builder)
		{
			builder.HasKey(p => new { p.NewsId, p.UserId });

			builder.HasOne(p => p.News)
				.WithMany(p => p.Bookmarks)
				.HasForeignKey(p => p.NewsId);
		}
	}
}
