using NewsManagement.Domain.NewsAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsWebsite.Entities;

namespace NewsManagement.Domain.NewsNewsCategoryAgg
{
    public class NewsNewsCategory
    {
        public long NewsId { get; set; }
        public int NewsCategoryId { get; set; }

        public virtual News News { get; set; }
        public virtual NewsCategory NewsCategory { get; set; }
    }
}
