using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        public NewsApplication( IFileUploader fileUploader, INewsRepository newsRepository, ITagRepository tagRepository)
        {
            
            _fileUploader = fileUploader;
            _newsRepository = newsRepository;
            _tagRepository = tagRepository;
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

        public (List<NewsViewModel>, long) Search(NewsSearchModel searchModel)
        {
            return _newsRepository.Search(searchModel);
        }

        private List<long> getNewsTagsId(string tags)
        {
            var tagArray = tags.Split(",");
            var allTags = _tagRepository.Get().Where(p => tags.Contains(p.TagName)).Select(p => p.Id).ToList();

            return allTags;
        }
    }
}
