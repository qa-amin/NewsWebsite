using NewsManagement.Domain.NewsAgg;
using NewsManagement.Domain.TagAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Domain.NewsTagAgg
{
    public class NewsTag
    {
        public long NewsId { get; set; }
        public long TagId { get; set; }

        public News News { get; set; }
        public Tag Tag { get; set; }

    }
}
