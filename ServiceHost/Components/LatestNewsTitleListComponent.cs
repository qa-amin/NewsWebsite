using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Domain.NewsAgg;

namespace ServiceHost.Components
{
	public class LatestNewsTitleListComponent : ViewComponent
	{
		private readonly INewsRepository _newsRepository;

        public LatestNewsTitleListComponent(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public IViewComponentResult Invoke()
		{
			var news = _newsRepository.Get().Where(p => p.IsPublish && p.PublishDateTime <= DateTime.Now).Select(p => new NewsViewModel()
            {
				Id = p.Id,
				Title = p.Title,
				Url = p.Url,
            }).ToList();
			return View(news);
		}
	}
}
