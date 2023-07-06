using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsManagement.Domain.NewsCategoryAgg;
using NewsWebsite.Entities;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
	public class NewsCategoryRepository : RepositoryBase<int,NewsCategory> , INewsCategoryRepository
	{
		private readonly NewsManagementDbContext _context;

		public NewsCategoryRepository(NewsManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public NewsCategoryViewModel FindByCategoryName(string categoryName)
        {
            var categoryViewModel = _context.NewsCategories
                .Select(p => new NewsCategoryViewModel
                {
                    CategoryName = p.CategoryName,
                    Url = p.Url,
                    ParentCategoryName = p.category.CategoryName,
                    CategoryId = p.Id,
					
                })
                .FirstOrDefault(p => p.CategoryName == categoryName);

            return categoryViewModel;
        }

		public List<NewsCategoryTreeViewModel> GetAllCategories()
		{
			var categories = _context.NewsCategories
				.Where(p => p.ParentCategoryId == null)
				.Select(p => new NewsCategoryTreeViewModel
				{
					id = p.Id,
					title = p.CategoryName,
					Url = p.Url
					
				}).ToList();

			foreach (var item in categories)
			{
				BindSubCategories(item);
			}

			return categories;
		}

		public (List<NewsCategoryViewModel>,int) Search(NewsCategorySearchModel searchModel)
		{
			var total = CountNewsCategory();

			var categories = new List<NewsCategoryViewModel>();

			if (!searchModel.Search.HasValue())
			{
				searchModel.Search = "";
			}

			if (searchModel.Limit == 0)
			{
				searchModel.Limit = total;
			}

			if (searchModel.Sort == "دسته")
			{
				if (searchModel.Order == "asc")
				{
					categories = GetPaginateCategories(searchModel.Offset, searchModel.Limit,
						true, null, searchModel.Search);
				}
				else
				{
					categories = GetPaginateCategories(searchModel.Offset, searchModel.Limit,
						false, null, searchModel.Search);
				}
			}
			else if (searchModel.Sort == "دسته پدر")
			{
				if (searchModel.Order == "asc")
				{
					categories = GetPaginateCategories(searchModel.Offset, searchModel.Limit,
						null, true, searchModel.Search);
				}
				else
				{
					categories = GetPaginateCategories(searchModel.Offset, searchModel.Limit,
						null, false, searchModel.Search);
				}
			}
			else
			{
				categories = GetPaginateCategories(searchModel.Offset, searchModel.Limit,
					null, null, searchModel.Search);
			}

			if (searchModel.Search != "")
				total = categories.Count();

			return (categories, total);
		}

		public int CountNewsCategory()
		{
			return _context.NewsCategories.Count();
		}

        public EditNewsCategory GetDetails(int id)
        {
            var newsCategory = _context.NewsCategories
                .Select(p => new EditNewsCategory
                {
                    CategoryName = p.CategoryName,
                    Url = p.Url,
                    Id = p.Id,
                    ParentCategoryName = p.category.CategoryName
				}).FirstOrDefault(p => p.Id == id);

            return newsCategory;
        }

        public List<NewsCategory> findChildCategories(int id)
        {
           return _context.NewsCategories.Where(p => p.ParentCategoryId == id).ToList();
        }


        private void BindSubCategories(NewsCategoryTreeViewModel category)
		{
			var subCategories = _context.NewsCategories
				.Where(p => p.ParentCategoryId == category.id)
				.Select( p => new NewsCategoryTreeViewModel
				{
					id = p.Id,
					title = p.CategoryName,
					Url = p.Url

                }).ToList();

			foreach (var item in subCategories)
			{
				BindSubCategories(item);
				category.subs.Add(item);
			}
		}

		private  List<NewsCategoryViewModel> GetPaginateCategories(int offset, int limit, bool? categoryNameSortAsc,
			bool? parentCategoryNameSortAsc, string searchText)
		{
			

			var categories = new List<NewsCategoryViewModel>();

			if (categoryNameSortAsc != null)
			{
				 categories = _context.NewsCategories
					 .Include(p => p.category)
					 .Where(p => p.CategoryName.Contains(searchText) || p.category.CategoryName.Contains(searchText))
					.Select(p => new NewsCategoryViewModel
					{
						CategoryId = p.Id,
						CategoryName = p.CategoryName,
						Url = p.Url,
						ParentCategoryName = p.category.CategoryName != null ? p.category.CategoryName : "-"
					})
					.OrderBy(c => (categoryNameSortAsc == true && categoryNameSortAsc != null) ? c.CategoryName : "")
					.OrderByDescending(c => (categoryNameSortAsc == false && categoryNameSortAsc != null) ? c.CategoryName : "")
					.Skip(offset).Take(limit)
					.ToList();
			}
			else if (parentCategoryNameSortAsc != null)
			{
				categories = _context.NewsCategories
					.Include(p => p.category)
					.Where(p => p.CategoryName.Contains(searchText) || p.category.CategoryName.Contains(searchText))
					.Select(p => new NewsCategoryViewModel
					{
						CategoryId = p.Id,
						CategoryName = p.CategoryName,
						Url = p.Url,
						ParentCategoryName = p.category.CategoryName != null ? p.category.CategoryName : "-"
					})
					.OrderBy(c => (parentCategoryNameSortAsc == true && parentCategoryNameSortAsc != null) ? c.ParentCategoryName : "")
					.OrderByDescending(c => (parentCategoryNameSortAsc == false && parentCategoryNameSortAsc != null) ? c.ParentCategoryName : "")
					.Skip(offset).Take(limit)
					.ToList();
			}
			else
			{
				categories = _context.NewsCategories
					.Include(p => p.category)
					.Where(p => p.CategoryName.Contains(searchText) || p.category.CategoryName.Contains(searchText))
					.Select(p => new NewsCategoryViewModel
					{
						CategoryId = p.Id,
						CategoryName = p.CategoryName,
						Url = p.Url,
						ParentCategoryName = p.category.CategoryName != null ? p.category.CategoryName : "-"
					})
					.Skip(offset).Take(limit)
					.ToList();
			}

			foreach (var item in categories)
			{
				item.Row = ++offset;
			}
				return categories;
		}
	}
}
