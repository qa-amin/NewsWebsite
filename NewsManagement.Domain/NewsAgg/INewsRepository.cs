﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using NewsManagement.Application.Contrasts.News;

namespace NewsManagement.Domain.NewsAgg
{
    
    public interface INewsRepository : IRepository<long, News>
    {
        (List<NewsViewModel>, int) Search(NewsSearchModel searchModel);

        void Update(News Enitiy);

        List<NewsViewModel> GetPaginateNews(int offset, int limit, string orderBy, string searchText,
            bool? isPublish, bool? isInternal);

        long CountNewsPublished();
    }
}
