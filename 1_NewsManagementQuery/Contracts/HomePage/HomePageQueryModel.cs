using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Application.Contrasts.Video;

namespace _1_NewsManagementQuery.Contracts.HomePageViewModel
{
	public class HomePageQueryModel
	{
		public HomePageQueryModel(List<NewsViewModel> news, List<NewsViewModel> mostViewedNews, List<NewsViewModel> mostTalkNews, List<NewsViewModel> mostPopularNews, List<NewsViewModel> internalNews, List<NewsViewModel> foreignNews, List<VideoViewModel> video, long countNewsPublished)
		{
			News = news;
			MostViewedNews = mostViewedNews;
			MostTalkNews = mostTalkNews;
			MostPopularNews = mostPopularNews;
			InternalNews = internalNews;
			ForeignNews = foreignNews;
			Video = video;
			CountNewsPublished = countNewsPublished;
		}

		public List<NewsViewModel> News { get; set; }
		public List<NewsViewModel> MostViewedNews { get; set; }
		public List<NewsViewModel> MostTalkNews { get; set; }
		public List<NewsViewModel> MostPopularNews { get; set; }
		public List<NewsViewModel> InternalNews { get; set; }
		public List<NewsViewModel> ForeignNews { get; set; }
		public List<VideoViewModel> Video { get; set; }
		public long CountNewsPublished { get; set; }
	}
}
