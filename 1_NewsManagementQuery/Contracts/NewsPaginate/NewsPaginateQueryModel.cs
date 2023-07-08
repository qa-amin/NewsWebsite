using NewsManagement.Application.Contrasts.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_NewsManagementQuery.Contracts.NewsPaginate
{
    public class NewsPaginateQueryModel
    {
        public NewsPaginateQueryModel(long newsCount, List<NewsViewModel> news)
        {
            NewsCount = newsCount;
            News = news;
        }

        public long NewsCount { get; set; }
        public List<NewsViewModel> News { get; set; }
    }
}
