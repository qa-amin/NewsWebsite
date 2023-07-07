using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsManagement.Domain.NewsNewsCategoryAgg;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
	public class NewsNewsCategoryRepository : INewsNewsCategoryRepository
	{
		private readonly NewsManagementDbContext _context;

		public NewsNewsCategoryRepository(NewsManagementDbContext context)
		{
			_context = context;
		}

		public List<NewsNewsCategory> Get()
		{
			return _context.NewsNewsCategories.ToList();
		}

		public void RemoveRange(List<NewsNewsCategory> entity)
		{
			_context.RemoveRange(entity);
		}
	}
}
