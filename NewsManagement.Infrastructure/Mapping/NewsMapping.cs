using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsManagement.Domain.NewsAgg;

namespace NewsManagement.Infrastructure.EFCore.Mapping
{
    public class NewsMapping : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasQueryFilter(p => !p.IsRemove);

            builder.Property(p => p.Title).HasMaxLength(500);
            builder.Property(p => p.Abstract).HasMaxLength(2000);
            
        }
    }
}
