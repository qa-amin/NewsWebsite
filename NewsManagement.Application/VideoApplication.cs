using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using NewsManagement.Application.Contrasts.Tag;
using NewsManagement.Application.Contrasts.Video;
using NewsManagement.Domain.VideoAgg;
using NewsWebsite.Common;

namespace NewsManagement.Application
{
    public class VideoApplication : IVideoApplication
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IFileUploader _fileUploader;

        public VideoApplication(IVideoRepository videoRepository, IFileUploader fileUploader)
        {
            _videoRepository = videoRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateVideo command)
        {
            var operation = new OperationResult();

            if (_videoRepository.Exists(p => p.Title == command.Title))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var path = $"VideoPoster";
            var posterPath = _fileUploader.Upload(command.PosterFile, path);

            var publishDate = DateTime.Now;

            var video = new Video(command.Title, command.Url, posterPath,publishDate);

            _videoRepository.Create(video);
            _videoRepository.SaveChanges();

            return operation.Succeeded(ApplicationMessages.videoAded);
        }

        public OperationResult Edit(EditVideo command)
        {
            var operation = new OperationResult();

            if (_videoRepository.Exists(p => p.Title == command.Title && p.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var posterPath = command.Poster;
            if (command.PosterFile != null)
            {
                var path = $"VideoPoster";
                posterPath = _fileUploader.Upload(command.PosterFile, path);
            }

            var publishDate = DateTime.Now;

            var video = _videoRepository.Get(command.Id);
            if (video == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            video.Edit(command.Title,command.Url,posterPath,publishDate);
            _videoRepository.SaveChanges();

            
            return operation.Succeeded(ApplicationMessages.VideoEdited);
        }

        public OperationResult Delete(long id)
        {
            var operation = new OperationResult();

            var video = _videoRepository.Get(id);

            if (video == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            video.Delete();

            _videoRepository.SaveChanges();

            return operation.Succeeded(ApplicationMessages.DeleteVideo);

        }

        public (List<VideoViewModel>, int) Search(VideoSearchModel searchModel)
        {
            var (videos, total) = _videoRepository.Search(searchModel);

            return (videos, total);
        }

        public EditVideo GetDetails(long id)
        {
            return _videoRepository.GetDetail(id);
        }
    }
}
