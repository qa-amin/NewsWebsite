using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Domain.NewsAgg;
using NewsManagement.Domain.NewsNewsCategoryAgg;
using NewsManagement.Domain.NewsTagAgg;
using NewsManagement.Domain.TagAgg;
using NewsWebsite.Common;

namespace NewsManagement.Application
{
    public class NewsApplication : INewsApplication
    {
        private readonly INewsRepository _newsRepository;
        private readonly IFileUploader _fileUploader;
        private readonly ITagRepository _tagRepository;
        private readonly INewsNewsCategoryRepository _newsNewsCategoryRepository;
        private readonly INewsTagRepository _newsTagRepository;

        public NewsApplication( IFileUploader fileUploader, INewsRepository newsRepository, ITagRepository tagRepository, INewsNewsCategoryRepository newsNewsCategoryRepository, INewsTagRepository newsTagRepository)
        {
            
            _fileUploader = fileUploader;
            _newsRepository = newsRepository;
            _tagRepository = tagRepository;
            _newsNewsCategoryRepository = newsNewsCategoryRepository;
            _newsTagRepository = newsTagRepository;
        }

        public OperationResult Create(CreateNews command)
        {
            var operation = new OperationResult();

            if(_newsRepository.Exists(p => (p.Title == command.Title) || (p.Url == command.Url)))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            if (command.SubmitButton != "ذخیره پیش نویس")
            {
                command.IsPublish = true;
            }

            if (command.IsPublish == true)
            {
                if (!command.PersianPublishDate.HasValue())
                    command.PublishDateTime = DateTimeExtensions.DateTimeWithOutMilliSecends(DateTime.Now);
                else
                {
                    var persianTimeArray = command.PersianPublishTime.Split(':');
                    command.PublishDateTime = command.PersianPublishDate.ConvertShamsiToMiladi().Date + new TimeSpan(int.Parse(persianTimeArray[0]), int.Parse(persianTimeArray[1]), 0);
                }
            }

            var newsNewsCategory = new List<NewsNewsCategory>();
            if (command.CategoryIds != null)
            {
                 newsNewsCategory = command.CategoryIds
                    .Select(p => new NewsNewsCategory
                    {
                        NewsCategoryId = p
                    }).ToList();


            }
            else
            {
                newsNewsCategory = null;
            }

            var newsTag = new List<NewsTag>();
            var tagsId = getNewsTagsId(command.NameOfTags);
            if (tagsId != null)
            {
                newsTag = tagsId.Select(p => new NewsTag
                {
                    TagId = p
                }).ToList();
            }
            else
            {
                newsTag = null;
            }

            if (command.ImageFile != null)
            {
                command.ImageName = $"news-{StringExtensions.GenerateId(10)}.jpg";
            }

            var path = "NewsImage";

            var imagePath = _fileUploader.UploadFileBase64(command.ImageFile, command.ImageName, path);

            var news = new News(command.Title, command.Description,command.PublishDateTime,
                command.UserId,command.Url,imagePath,command.IsPublish,command.IsInternal,
                command.Abstract, newsNewsCategory,newsTag);


            _newsRepository.Create(news);
            _newsRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditNews command)
        {
			var operation = new OperationResult();

			if (_newsRepository.Exists(p => ((p.Title == command.Title || p.Url == command.Url) && p.Id != command.Id)))
			{
				return operation.Failed(ApplicationMessages.DuplicatedRecord);
			}


			if (command.SubmitButton != "ذخیره پیش نویس")
			{
				command.IsPublish = true;
			}

			if (command.IsPublish == true)
			{
				if (!command.PersianPublishDate.HasValue())
					command.PublishDateTime = DateTimeExtensions.DateTimeWithOutMilliSecends(DateTime.Now);
				else
				{
					var persianTimeArray = command.PersianPublishTime.Split(':');
					command.PublishDateTime = command.PersianPublishDate.ConvertShamsiToMiladi().Date + new TimeSpan(int.Parse(persianTimeArray[0]), int.Parse(persianTimeArray[1]), 0);
				}
			}

			var newsNewsCategory = new List<NewsNewsCategory>();
			if (command.CategoryIds != null)
			{
				newsNewsCategory = command.CategoryIds
				   .Select(p => new NewsNewsCategory
				   {
					   NewsCategoryId = p
				   }).ToList();


			}
			else
			{
				newsNewsCategory = null;
			}

			var newsTag = new List<NewsTag>();
			var tagsId = getNewsTagsId(command.NameOfTags);
			if (tagsId != null)
			{
				newsTag = tagsId.Select(p => new NewsTag
				{
					TagId = p
				}).ToList();
			}
			else
			{
				newsTag = null;
			}

			var imagePath = command.ImageName;
			if (command.ImageFile != null)
			{
				command.ImageName = $"news-{StringExtensions.GenerateId(10)}.jpg";
				var path = "NewsImage";

				imagePath = _fileUploader.UploadFileBase64(command.ImageFile, command.ImageName, path);
			}

			var news = _newsRepository.Get(command.Id);
			if (news == null)
			{
				return operation.Failed(ApplicationMessages.UserNotFound);
			}

			 news.Edit(command.Title, command.Description, command.PublishDateTime,
				command.UserId, command.Url, imagePath, command.IsPublish, command.IsInternal,
				command.Abstract, newsNewsCategory, newsTag);

			 RemoveNewsTagRecords(news.Id);
             RemoveNewsNewsCategoryRecords(news.Id);

			 _newsRepository.Update(news);


			_newsRepository.SaveChanges();


            return operation.Succeeded(ApplicationMessages.EditedNews);
		}

        public OperationResult Delete(long id)
        {
            var operation = new OperationResult();
            var user = _newsRepository.Get(id);
            if (user == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            user.Delete();
            _newsRepository.SaveChanges();
            return operation.Succeeded(ApplicationMessages.DeleteNews);
        }

        public (List<NewsViewModel>, long) Search(NewsSearchModel searchModel)
        {
            return _newsRepository.Search(searchModel);
        }

        public EditNews GetDetails(long id)
        {
            var news = _newsRepository.Get(id);
            return new EditNews()
            {
                IsInternal = news.IsInternal,
                Title = news.Title,
                Description = news.Description,
                Abstract = news.Abstract,
                Id = news.Id,
                UserId = news.UserId,
                ImageName = news.ImageName,
                PublishDateTime = news.PublishDateTime,
                PersianPublishDate = news.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd"),
                PersianPublishTime = news.PublishDateTime.Value.ToString("HH:mm"),
                FuturePublish = news.PublishDateTime > DateTime.Now ? true : false,
				IsPublish = news.IsPublish,
                Url = news.Url,
                
                
                

            };
        }

        public long[] CategoryIds(long newsId)
        {
	        var newsNewsCategories = _newsNewsCategoryRepository.Get()
		        .Select(p => new { p.NewsCategoryId ,p.NewsId})
		        .Where(p => p.NewsId == newsId).ToList();

	        var newsCategoryId = newsNewsCategories.Select(p => (long)p.NewsCategoryId).ToArray();
	        return newsCategoryId;

        }

        public string GetTagNames(long newsId)
        {
	        var newsTag = _newsTagRepository.Get().Where(p => p.NewsId == newsId).Select(p => new { p.NewsId, p.TagId }).ToList();
	        var tags = _tagRepository.Get().Select(p => new { p.Id, p.TagName }).ToList();

            var tagNames = newsTag.Join(tags,
	            nt => nt.TagId,
	            t => t.Id,
	            (nt, t) => new
	            {
                    tagName = t.TagName
	            }).ToList();

            var strTagName = tagNames.Select(p => p.tagName).ToArray().CombineWith(',');


			return strTagName;

        }

        public void RemoveNewsTagRecords(long newsId)
        {
	        var newsTag = _newsTagRepository.Get().Where(p => p.NewsId == newsId).ToList();
            _newsTagRepository.RemoveRange(newsTag);
        }

        public void RemoveNewsNewsCategoryRecords(long newsId)
        {
	        var newsTag = _newsNewsCategoryRepository.Get().Where(p => p.NewsId == newsId).ToList();
            _newsNewsCategoryRepository.RemoveRange(newsTag);
        }

        private List<long> getNewsTagsId(string tags)
        {
            var tagArray = tags.Split(",");
            var listTags = _tagRepository.Get();
            foreach (var item in tagArray)
            {
                if (listTags.All(p => p.TagName != item))
                {
                    var tag = new Tag(item);
                    _tagRepository.Create(tag);
                    _tagRepository.SaveChanges();
                }
                
            }
            var allTags = _tagRepository.Get().Where(p => tags.Contains(p.TagName)).Select(p => p.Id).ToList();

            return allTags;
        }
    }
}
