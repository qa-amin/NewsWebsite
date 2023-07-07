using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Domain.NewsNewsCategoryAgg
{
	public interface INewsNewsCategoryRepository
	{
		List<NewsNewsCategory> Get();
		void RemoveRange(List<NewsNewsCategory> entity);
	}
}
