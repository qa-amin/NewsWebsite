using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Domain.BookMarkAgg;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
	public class BookMarkRepository : RepositoryBase<long,BookMark>, IBookMarkRepository
	{
		private readonly NewsManagementDbContext _context;

		public BookMarkRepository( NewsManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public BookMark GetBookMark(long newsId, long userId)
		{
			var bookMark = _context.BookMarks.FirstOrDefault(p => p.NewsId == newsId && p.UserId == userId);
			return bookMark;
		}

		public void Delete(BookMark entity)
		{
			_context.BookMarks.Remove(entity);
		}
	}
}
