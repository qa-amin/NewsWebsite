using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_NewsManagementQuery.Contracts.NewsPaginate
{
	public interface  INewsPaginateQuery
	{
		NewsPaginateQueryModel GetNewsPaginate(int limit, int offset);
	}
}
