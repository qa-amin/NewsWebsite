using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using NewsManagement.Domain.CommentAgg;
using NewsManagement.Domain.NewsNewsCategoryAgg;
using NewsManagement.Domain.NewsTagAgg;
using NewsWebsite.Entities;

namespace NewsManagement.Domain.NewsAgg
{
    public class News : EntityBase<long>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime? PublishDateTime { get; private set; }
        public long UserId { get; private set; }
        public string Url { get; private set; }
        public string ImageName { get; private set; }
        public bool IsPublish { get; private set; }
        public bool IsInternal { get; private set; }
        public string Abstract { get; private set; }


        public virtual ICollection<NewsTag> NewsTags { get; set; }
        public virtual ICollection<NewsNewsCategory> NewsNewsCategories { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public News()
        {
            
        }

        public News(string title, string description, DateTime? publishDateTime, long userId,
            string url,string imageName, bool isPublish, bool isInternal,
            string Abstract, List<NewsNewsCategory> categories,
            List<NewsTag> tags)
        {
            Title = title;
            Description = description;
            PublishDateTime = publishDateTime;
            UserId = userId;
            Url = url;
            ImageName = imageName;
            IsPublish = isPublish;
            IsInternal = isInternal;
            this.Abstract = Abstract;
            NewsNewsCategories = categories;
            NewsTags = tags;
        }

		public void Edit(string title, string description, DateTime? publishDateTime, long userId,
			string url, string imageName, bool isPublish, bool isInternal,
			string Abstract, List<NewsNewsCategory> categories,
			List<NewsTag> tags)
		{
			Title = title;
			Description = description;
			PublishDateTime = publishDateTime;
			UserId = userId;
			Url = url;
			ImageName = imageName;
			IsPublish = isPublish;
			IsInternal = isInternal;
			this.Abstract = Abstract;
			NewsNewsCategories = categories;
			NewsTags = tags;
            UpdateDate = DateTime.Now;
		}

		public void Delete()
        {
            IsRemove = true;
            RemoveDate = DateTime.Now;
        }
    }
}
