using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsWebsite.Entities;

namespace NewsManagement.Infrastructure.EFCore.Mapping
{
	public class NewsCategoryMapping : IEntityTypeConfiguration<NewsCategory>
	{
		public void Configure(EntityTypeBuilder<NewsCategory> builder)
		{
			builder.HasKey(p => p.Id);
            builder.HasQueryFilter(p => !p.IsRemove);

			builder.HasOne(p => p.category)
				.WithMany(p => p.Categories)
				.HasForeignKey(p => p.ParentCategoryId);
		}
	}
}
