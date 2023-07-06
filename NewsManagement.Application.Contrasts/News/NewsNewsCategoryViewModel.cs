using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsManagement.Application.Contrasts.NewsCategory;

namespace NewsManagement.Application.Contrasts.News
{
    public class NewsNewsCategoryViewModel
    {
        public NewsNewsCategoryViewModel(List<NewsCategoryTreeViewModel> categories, long[] categoryIds)
        {
            Categories = categories;
            CategoryIds = categoryIds;
        }

        public List<NewsCategoryTreeViewModel> Categories { get; set; }
        public long[] CategoryIds { get; set; }
    }
}
