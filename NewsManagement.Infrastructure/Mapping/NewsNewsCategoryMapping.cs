using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsManagement.Domain.NewsNewsCategoryAgg;

namespace NewsManagement.Infrastructure.EFCore.Mapping
{
    public class NewsNewsCategoryMapping : IEntityTypeConfiguration<NewsNewsCategory>
    {
        public void Configure(EntityTypeBuilder<NewsNewsCategory> builder)
        {
            builder.HasKey(t => new { t.NewsCategoryId, t.NewsId });
            builder
                .HasOne(p => p.News)
                .WithMany(t => t.NewsNewsCategories)
                .HasForeignKey(f => f.NewsId);

            builder
                .HasOne(p => p.NewsCategory)
                .WithMany(t => t.NewsNewsCategories)
                .HasForeignKey(f => f.NewsCategoryId);
        }
    }
}
