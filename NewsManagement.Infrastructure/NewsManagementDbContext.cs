﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Domain.BookMarkAgg;
using NewsManagement.Domain.CommentAgg;
using NewsManagement.Domain.LikeAgg;
using NewsManagement.Domain.NewsAgg;
using NewsManagement.Domain.NewsLetterAgg;
using NewsManagement.Domain.NewsNewsCategoryAgg;
using NewsManagement.Domain.NewsTagAgg;
using NewsManagement.Domain.TagAgg;
using NewsManagement.Domain.VideoAgg;
using NewsManagement.Domain.VisitAgg;
using NewsManagement.Infrastructure.EFCore.Mapping;
using NewsWebsite.Entities;

namespace NewsManagement.Infrastructure.EFCore
{
	public class NewsManagementDbContext : DbContext
	{
		public DbSet<NewsCategory> NewsCategories { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Video> Videos { get; set; }
		public DbSet<News> News { get; set; }
		public DbSet<NewsNewsCategory> NewsNewsCategories { get; set; }
		public DbSet<NewsTag> NewsTags { get; set; }
		public DbSet<NewsLetter> NewsLetters { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Visit> Visits {  get; set; }
		public DbSet<BookMark> BookMarks {  get; set; }


        public NewsManagementDbContext(DbContextOptions<NewsManagementDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new NewsCategoryMapping());
            modelBuilder.ApplyConfiguration(new TagMapping());
            modelBuilder.ApplyConfiguration(new VideoMapping());
            modelBuilder.ApplyConfiguration(new NewsMapping());
            modelBuilder.ApplyConfiguration(new NewsTagMapping());
            modelBuilder.ApplyConfiguration(new NewsNewsCategoryMapping());
            modelBuilder.ApplyConfiguration(new NewsLetterMapping());
			modelBuilder.ApplyConfiguration(new CommentMapping());
			modelBuilder.ApplyConfiguration(new LikeMapping());
			modelBuilder.ApplyConfiguration(new VisitMapping());
			modelBuilder.ApplyConfiguration(new BookMarkMapping());
		}
	}
}
