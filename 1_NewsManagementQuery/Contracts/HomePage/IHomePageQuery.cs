using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_NewsManagementQuery.Contracts.HomePageViewModel;
using NewsManagement.Application.Contrasts.News;

namespace _1_NewsManagementQuery.Contracts.HomePage
{
    public interface IHomePageQuery
    {
        HomePageQueryModel GetNews();
        List<NewsViewModel> MostViewedNews(int offset, int limit, string duration);
        List<NewsViewModel> MostTalkNews(int offset, int limit, string duration); 

    }
}
