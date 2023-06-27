using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Infrastructure.EFCore.Mapping;
using NewsWebsite.Entities;

namespace NewsManagement.Infrastructure.EFCore
{
	public class NewsManagementDbContext : DbContext
	{
		public DbSet<NewsCategory> NewsCategories { get; set; }

		public NewsManagementDbContext(DbContextOptions<NewsManagementDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new NewsCategoryMapping());
		}
	}
}
