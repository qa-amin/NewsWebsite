using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsManagement.Domain.NewsCategoryAgg;

namespace ServiceHost.Components
{
	public class CategoryListComponent : ViewComponent
    {
        private readonly INewsCategoryRepository _newsCategoryRepository;

        public CategoryListComponent(INewsCategoryRepository newsCategoryRepository)
        {
            _newsCategoryRepository = newsCategoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var newsCategory = _newsCategoryRepository.GetAllCategories();
			return View(newsCategory);
		}
	}
}
