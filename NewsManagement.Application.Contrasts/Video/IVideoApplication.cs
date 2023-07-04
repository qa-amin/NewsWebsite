using _0_Framework.Application;
using NewsManagement.Application.Contrasts.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.Video
{
    public interface IVideoApplication
    {
        OperationResult Create(CreateVideo command);
        OperationResult Edit(EditVideo command);
        OperationResult Delete(long id);

        (List<VideoViewModel>, int) Search(VideoSearchModel searchModel);

        EditVideo GetDetails(long id);
    }
}
