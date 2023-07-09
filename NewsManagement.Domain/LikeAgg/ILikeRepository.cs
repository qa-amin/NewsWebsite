using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace NewsManagement.Domain.LikeAgg
{
	public interface ILikeRepository : IRepository<long,Like>
	{
		Like GetLike(long newsId, string ip);
	}
}
