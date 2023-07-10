using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_NewsManagementQuery.Contracts.HomePage;
using _1_NewsManagementQuery.Contracts.HomePageViewModel;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Domain.NewsAgg;
using NewsManagement.Domain.VideoAgg;

namespace _1_NewsManagementQuery.Query
{
    public class HomePageQuery : IHomePageQuery
    {
        private readonly INewsRepository _newsRepository;
        private readonly IVideoRepository _videoRepository;

        public HomePageQuery(INewsRepository newsRepository, IVideoRepository videoRepository)
        {
            _newsRepository = newsRepository;
            _videoRepository = videoRepository;
        }


        public HomePageQueryModel GetNews()
        {
            var news = _newsRepository.GetPaginateNews(0, 10, "PublishDateTime desc", "", true, null);
            var videos = _videoRepository.GetPaginateVideos(0, 10, "PublishDateTime desc", "");
            var internalNews = _newsRepository.GetPaginateNews(0, 10, "PublishDateTime desc", "", true, true);
            var foreignNews = _newsRepository.GetPaginateNews(0, 10, "PublishDateTime desc", "", true, false);
            var mostViewedNews = _newsRepository.MostViewedNews(0, 3, "day");
            var mostTalkNews = _newsRepository.MostTalkNews(0, 3, "day");
            var mostPopularNews = _newsRepository.MostPopularNews(0, 5);

            var countNewsPublished = _newsRepository.CountNewsPublished();
            
            var homePageQueryModel = new HomePageQueryModel(news, mostViewedNews, mostTalkNews, mostPopularNews, internalNews,foreignNews,videos, countNewsPublished);
            
            return homePageQueryModel;
        }

        public  List<NewsViewModel> MostViewedNews(int offset, int limit, string duration)
        {
	        var mostViewedNews =  _newsRepository.MostViewedNews(offset, limit, duration);
			return mostViewedNews;
        }

        public List<NewsViewModel> MostTalkNews(int offset, int limit, string duration)
        {
            var mostTalkNews = _newsRepository.MostTalkNews(offset, limit, duration);
            return mostTalkNews;
        }
    }
}
