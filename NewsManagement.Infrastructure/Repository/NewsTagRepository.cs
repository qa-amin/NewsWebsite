using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsManagement.Domain.NewsTagAgg;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
	public class NewsTagRepository : INewsTagRepository
	{
		private readonly NewsManagementDbContext _context;

		public NewsTagRepository(NewsManagementDbContext context)
		{
			_context = context;
		}

		public List<NewsTag> Get()
		{
			return _context.NewsTags.ToList();
		}

		public void RemoveRange(List<NewsTag> Entity)
		{
			_context.RemoveRange(Entity);
		}
	}
}
