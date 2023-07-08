using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_NewsManagementQuery.Contracts.HomePageViewModel;

namespace _1_NewsManagementQuery.Contracts.HomePage
{
    public interface IHomePageQuery
    {
        HomePageQueryModel GetNews();
    }
}
