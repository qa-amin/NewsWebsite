using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_NewsManagementQuery.Contracts.NewsInCategoriesAndTags
{
    public interface INewsInCategoriesAndTagsQuery
    {
        List<NewsInCategoriesAndTagsQueryModel> GetNewsInCategory(int categoryId, int pageIndex, int pageSize);
        List<NewsInCategoriesAndTagsQueryModel> GetNewsInTag(long tagId, int pageIndex, int pageSize);
    }
}
