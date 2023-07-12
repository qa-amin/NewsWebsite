using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_NewsManagementQuery.Contracts.NewsInCategoriesAndTags
{
    public class NewsInCategoriesAndTagsQueryModel
    {
        public long NewsId { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Abstract { get; set; }
        public DateTime? PublishDateTime { get; set; }
        public string PersianPublishDate { get; set; }
        public string PersianPublishTime { get; set; }
        public string AuthorName { get; set; }
        public string ImageName { get; set; }
        public int NumberOfVisit { get; set; }
        public int NumberOfLike { get; set; }
        public int NumberOfDisLike { get; set; }
        public int NumberOfComments { get; set; }
        public string NameOfCategories { get; set; }
        public string Url { get; set; }
    }
}
