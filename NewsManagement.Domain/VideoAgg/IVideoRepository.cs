using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using NewsManagement.Application.Contrasts.Video;

namespace NewsManagement.Domain.VideoAgg
{
    public interface IVideoRepository : IRepository<long,Video>
    {
        (List<VideoViewModel>, int) Search(VideoSearchModel searchModel);

        EditVideo GetDetail(long id);
    }
}
