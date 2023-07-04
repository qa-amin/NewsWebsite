using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsManagement.Domain.VideoAgg;

namespace NewsManagement.Infrastructure.EFCore.Mapping
{
    public class VideoMapping : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasQueryFilter(p => !p.IsRemove);

            builder.Property(p => p.Title).HasMaxLength(500);
        }
    }
}
