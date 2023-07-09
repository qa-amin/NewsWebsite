using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NewsManagement.Domain.LikeAgg;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
	public class LikeRepository : RepositoryBase<long,Like>, ILikeRepository
	{
		private readonly NewsManagementDbContext _context;

		public LikeRepository( NewsManagementDbContext context) : base(context)
		{
			_context = context;
		}


		public Like GetLike(long newsId, string ip)
		{
			var like = _context.Likes.FirstOrDefault(p => p.NewsId == newsId && p.IpAddress == ip);
			return like;
		}
	}
}
