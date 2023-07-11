using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_NewsManagementQuery.Contracts.NewsDetail
{
    public interface INewsDetailQuery
    {
        NewsDetailQueryModel GetDetail(long newsId, long? userId);
    }
}
