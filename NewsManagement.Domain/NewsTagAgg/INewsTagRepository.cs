using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Domain.NewsTagAgg
{
	public interface INewsTagRepository
	{
		List<NewsTag> Get();
		void RemoveRange(List<NewsTag> Entity);
	}
}
