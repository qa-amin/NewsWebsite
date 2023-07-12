using _1_NewsManagementQuery.Contracts.NewsInCategoriesAndTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsManagement.Domain.NewsAgg;
using NewsWebsite.Common;

namespace _1_NewsManagementQuery.Query
{
    public class NewsInCategoriesAndTagsQuery : INewsInCategoriesAndTagsQuery
    {
        private readonly INewsRepository _newsRepository;

        public NewsInCategoriesAndTagsQuery(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public List<NewsInCategoriesAndTagsQueryModel> GetNewsInCategory(int categoryId, int pageIndex, int pageSize)
        {
            var listNewsInCategory = _newsRepository.GetNewsinCategory(categoryId, pageIndex, pageSize)
                .Select(p => new NewsInCategoriesAndTagsQueryModel
                {
                    PublishDateTime = p.PublishDateTime,
                    Url = p.Url,
                    Abstract = p.Abstract,
                    AuthorName = p.AuthorName,
                    ImageName = p.ImageName,
                    NameOfCategories = p.NameOfCategories,
                    NewsId = p.Id,
                    NumberOfVisit = p.NumberOfVisit,
                    NumberOfComments = p.NumberOfComments,
                    NumberOfDisLike = p.NumberOfDisLike,
                    PersianPublishTime = p.PublishDateTime.ConvertMiladiToShamsi("HH:mm"),
                    NumberOfLike = p.NumberOfLike,
                    PersianPublishDate = p.PersianPublishDate,
                    ShortTitle = p.ShortTitle,
                    Title = p.Title,
                }).ToList();
            return listNewsInCategory;
        }

        public List<NewsInCategoriesAndTagsQueryModel> GetNewsInTag(long tagId, int pageIndex, int pageSize)
        {
            var listNewsInCategory = _newsRepository.GetNewsinTag(tagId, pageIndex, pageSize)
                .Select(p => new NewsInCategoriesAndTagsQueryModel
                {
                    PublishDateTime = p.PublishDateTime,
                    Url = p.Url,
                    Abstract = p.Abstract,
                    AuthorName = p.AuthorName,
                    ImageName = p.ImageName,
                    NameOfCategories = p.NameOfCategories,
                    NewsId = p.Id,
                    NumberOfVisit = p.NumberOfVisit,
                    NumberOfComments = p.NumberOfComments,
                    NumberOfDisLike = p.NumberOfDisLike,
                    PersianPublishTime = p.PublishDateTime.ConvertMiladiToShamsi("HH:mm"),
                    NumberOfLike = p.NumberOfLike,
                    PersianPublishDate = p.PersianPublishDate,
                    ShortTitle = p.ShortTitle,
                    Title = p.Title,
                }).ToList();
            return listNewsInCategory;
        }
    }
}
