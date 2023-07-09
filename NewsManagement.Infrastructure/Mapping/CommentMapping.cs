using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NewsManagement.Domain.CommentAgg;
using NewsManagement.Domain.NewsAgg;

namespace NewsManagement.Infrastructure.EFCore.Mapping
{
	public class CommentMapping : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(p => p.Name).HasMaxLength(500);


			builder.HasOne(p => p.comment)
				.WithMany(p => p.comments)
				.HasForeignKey(p => p.ParentCommentId);

			builder.HasOne(p => p.News)
				.WithMany(p => p.Comments)
				.HasForeignKey(p => p.NewsId);
		}
	}
}
