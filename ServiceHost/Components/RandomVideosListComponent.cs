using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.Video;
using NewsManagement.Domain.VideoAgg;

namespace ServiceHost.Components
{
	public class RandomVideosListComponent : ViewComponent
	{
		private readonly IVideoRepository _videoRepository;

        public RandomVideosListComponent(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public IViewComponentResult Invoke(int number)
        {
            var video = _videoRepository.Get();
            var videoCount = video.Count;
            var videoViewModels = video.Select(p => new VideoViewModel
            {
                Id = p.Id,
                Poster = p.Poster,
                Title = p.Title,
                Url = p.Url,
            }).ToList();
            var listRandomVideo = new List<VideoViewModel>();
            for (int i = 0; i < number; i++)
            {
                var randomNumber = CustomMethods.RandomNumber(1, videoCount + 1);
                
                listRandomVideo.Add(videoViewModels.Skip(randomNumber - 1).Take(1).FirstOrDefault());
            }
         
			return View(listRandomVideo);
		}
	}
}
