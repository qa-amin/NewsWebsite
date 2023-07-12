using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_NewsManagementQuery.Contracts.CategoryOrTagInfo;
using NewsManagement.Domain.NewsAgg;
using NewsManagement.Domain.NewsCategoryAgg;
using NewsManagement.Domain.TagAgg;

namespace _1_NewsManagementQuery.Query
{
    public class CategoryOrTagInfoQuery : ICategoryOrTagInfoQuery
    {
        private readonly ITagRepository _tagRepository;
        private readonly INewsCategoryRepository _newsCategoryRepository;
        

        public CategoryOrTagInfoQuery( INewsCategoryRepository newsCategoryRepository, ITagRepository tagRepository)
        {
            _newsCategoryRepository = newsCategoryRepository;
            _tagRepository = tagRepository;
        }

        public CategoryOrTagInfoQueryModel FindCategory(int categoryId)
        {
            var newsCategory = _newsCategoryRepository.Get(categoryId);
            if (newsCategory == null)
            {
                return null;
            }
            else
            {
                return new CategoryOrTagInfoQueryModel
                {
                    Id = newsCategory.Id,
                    Title = newsCategory.CategoryName,
                    IsCategory = true
                };
            }
        }

        public CategoryOrTagInfoQueryModel FindTag(long tagId)
        {
            var tag = _tagRepository.Get(tagId);
            if (tag == null)
            {
                return null;
            }
            else
            {
                return new CategoryOrTagInfoQueryModel
                {
                    Id = tag.Id,
                    IsCategory = false,
                    Title = tag.TagName
                };
            }
        }
    }
}
