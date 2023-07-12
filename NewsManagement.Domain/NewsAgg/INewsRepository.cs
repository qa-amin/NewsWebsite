using System;
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

        List<NewsViewModel> MostViewedNews(int offset, int limit, string duration);
        List<NewsViewModel> MostTalkNews(int offset, int limit, string duration);
        List<NewsViewModel> MostPopularNews(int offset, int limit);

        NewsViewModel GetNewsById(long newsId, long? userId);

        List<NewsViewModel> GetNextAndPreviousNews(DateTime? publishDateTime, long? userId);
        List<NewsViewModel> GetNewsinCategory(int categoryId, int pageIndex, int pageSize);
        List<NewsViewModel> GetNewsinTag(long tagId, int pageIndex, int pageSize);

        long CountNewsPublished();
    }
}
