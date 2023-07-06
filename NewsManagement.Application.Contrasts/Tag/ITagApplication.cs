using _0_Framework.Application;
using NewsManagement.Application.Contrasts.NewsCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.Tag
{
    public interface ITagApplication
    {
        OperationResult Create(CreateTag command);
        OperationResult Edit(EditTag command);
        OperationResult Delete(long id);

        (List<TagViewModel>, int) Search(TagSearchModel searchModel);

        EditTag GetDetails(long id);


        List<TagViewModel> GetAllTags();
    }
}
