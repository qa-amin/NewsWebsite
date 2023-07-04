using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Application.Contrasts.Video;
using NewsManagement.Domain.VideoAgg;
using NewsManagement.Infrastructure.EFCore.Migrations;
using NewsWebsite.Common;
using System.Linq.Dynamic.Core;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
    
    public class VideoRepository : RepositoryBase<long,Video> , IVideoRepository
    {
        private readonly NewsManagementDbContext _context;

        public VideoRepository( NewsManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public (List<VideoViewModel>, int) Search(VideoSearchModel searchModel)
        {
            List<VideoViewModel> videos;
            int total = _context.Videos.Count();
            if (!searchModel.Search.HasValue())
                searchModel.Search = "";

            if (searchModel.Limit == 0)
                searchModel.Limit = total;

            if (searchModel.Sort == "عنوان ویدیو")
            {
                if (searchModel.Order == "asc")
                    videos = GetPaginateVideos(searchModel.Offset, searchModel.Limit, "Title", searchModel.Search);
                else
                    videos = GetPaginateVideos(searchModel.Offset, searchModel.Limit, "Title desc", searchModel.Search);
            }

            else if (searchModel.Sort == "تاریخ انتشار")
            {
                if (searchModel.Order == "asc")
                    videos = GetPaginateVideos(searchModel.Offset, searchModel.Limit, "PublishDateTime", searchModel.Search);
                else
                    videos = GetPaginateVideos(searchModel.Offset, searchModel.Limit, "PublishDateTime desc", searchModel.Search);
            }

            else
                videos = GetPaginateVideos(searchModel.Offset, searchModel.Limit, "PublishDateTime desc", searchModel.Search);

            if (searchModel.Search != "")
                total = videos.Count();

            return (videos,total);
        }

        public EditVideo GetDetail(long id)
        {
            var video = _context.Videos.Select(p => new EditVideo
            {
                Id = p.Id,
                Url = p.Url,
                Title = p.Title,
                Poster = p.Poster

            }).FirstOrDefault(p => p.Id == id);

            return video;
        }


        public List<VideoViewModel> GetPaginateVideos(int offset, int limit, string orderBy, string searchText)
        {
            var getDateTimesForSearch = searchText.GetDateTimeForSearch();
            var videos =  _context.Videos.Where(c => c.Title.Contains(searchText) || (c.PublishDateTime >= getDateTimesForSearch.First() && c.PublishDateTime <= getDateTimesForSearch.Last()))
                .OrderBy(orderBy).Skip(offset).Take(limit)
                .Select(c => new VideoViewModel { Id = c.Id, Title = c.Title, Url = c.Url, Poster = c.Poster, PersianPublishDateTime = c.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm:ss"), PublishDateTime = c.PublishDateTime }).ToList();

            foreach (var item in videos)
                item.Row = ++offset;

            return videos;
        }
    }



}
