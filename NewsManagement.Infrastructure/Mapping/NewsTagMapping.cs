using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsManagement.Domain.NewsTagAgg;

namespace NewsManagement.Infrastructure.EFCore.Mapping
{
    public class NewsTagMapping : IEntityTypeConfiguration<NewsTag>
    {
        public void Configure(EntityTypeBuilder<NewsTag> builder)
        {
            builder.HasKey(t => new { t.TagId, t.NewsId });
            builder
                .HasOne(p => p.News)
                .WithMany(t => t.NewsTags)
                .HasForeignKey(f => f.NewsId);

            builder
                .HasOne(p => p.Tag)
                .WithMany(t => t.NewsTags)
                .HasForeignKey(f => f.TagId);
        }
    }
}
