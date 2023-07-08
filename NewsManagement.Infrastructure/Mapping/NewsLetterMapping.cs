using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsManagement.Domain.NewsLetterAgg;

namespace NewsManagement.Infrastructure.EFCore.Mapping
{
    public class NewsLetterMapping : IEntityTypeConfiguration<NewsLetter>
    {
        public void Configure(EntityTypeBuilder<NewsLetter> builder)
        {
            builder.HasKey(p => p.Email);
            builder.HasQueryFilter(p => !p.IsRemove);
        }
    }
}
