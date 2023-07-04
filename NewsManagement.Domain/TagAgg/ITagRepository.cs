using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using NewsManagement.Application.Contrasts.Tag;

namespace NewsManagement.Domain.TagAgg
{
    public interface ITagRepository : IRepository<long,Tag>
    {
        EditTag GetDetail(long id);
        (List<TagViewModel>, int) Search(TagSearchModel searchModel);
    }
}
