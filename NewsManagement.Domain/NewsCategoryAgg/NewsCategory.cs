using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using _0_Framework.Domain;
using NewsManagement.Domain.NewsNewsCategoryAgg;

namespace NewsWebsite.Entities
{
    public class NewsCategory : EntityBase<int>
    {

	    
		public string CategoryName { get; private set; }
		public string Url { get; private set; }

		public int? ParentCategoryId { get; private set; }
		public virtual NewsCategory category { get; private set; }

		public virtual List<NewsCategory> Categories { get; private set; }

        public ICollection<NewsNewsCategory> NewsNewsCategories { get; set; }

        public NewsCategory(string categoryName, string url, int? parentCategoryId)
        {
            CategoryName = categoryName;
            Url = url;
            ParentCategoryId = parentCategoryId;
        }
        public void Edit(string categoryName, string url, int? parentCategoryId)
        {
            CategoryName = categoryName;
            Url = url;
            ParentCategoryId = parentCategoryId;
            UpdateDate = DateTime.Now;
        }

        public void Delete()
        {
            IsRemove = true;
            RemoveDate = DateTime.Now;
        }

      


    }
}
