using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace NewsManagement.Application.Contrasts.News
{
    public interface INewsApplication
    {
        OperationResult Create(CreateNews command );
        (List<NewsViewModel>, long) Search(NewsSearchModel searchModel);
    }
}
