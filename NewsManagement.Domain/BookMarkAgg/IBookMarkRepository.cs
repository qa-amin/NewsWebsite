using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace NewsManagement.Domain.BookMarkAgg
{
	public interface IBookMarkRepository : IRepository<long ,BookMark>
	{
		BookMark GetBookMark(long newsId, long userId);
		void Delete(BookMark entity);
	}
}
