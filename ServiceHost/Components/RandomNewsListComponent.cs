using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Application.Contrasts.Video;
using NewsManagement.Domain.NewsAgg;

namespace ServiceHost.Components
{
	public class RandomNewsListComponent : ViewComponent
	{
		private readonly INewsRepository _newsRepository;

        public RandomNewsListComponent(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public IViewComponentResult Invoke(int number)
		{
            var news = _newsRepository.Get();
            var newsCount = news.Count;
            var newsViewModels = news.Select(p => new NewsViewModel
            {
                Id = p.Id,
                ImageName = p.ImageName,
                Title = p.Title,
                Url = p.Url,
            }).ToList();
            var listRandomNews = new List<NewsViewModel>();
            for (int i = 0; i < number; i++)
            {
                var randomNumber = CustomMethods.RandomNumber(1, newsCount + 1);

                listRandomNews.Add(newsViewModels.Skip(randomNumber - 1).Take(1).FirstOrDefault());
            }
            return View(listRandomNews);
		}
	}
}
