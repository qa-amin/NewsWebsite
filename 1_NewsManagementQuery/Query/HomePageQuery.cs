using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_NewsManagementQuery.Contracts.HomePage;
using _1_NewsManagementQuery.Contracts.HomePageViewModel;
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
            var countNewsPublished = _newsRepository.CountNewsPublished();
            
            var homePageQueryModel = new HomePageQueryModel(news,null,null,null,internalNews,foreignNews,videos, countNewsPublished);
            
            return homePageQueryModel;
        }
    }
}
