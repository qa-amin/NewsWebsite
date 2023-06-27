using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace NewsManagement.Application.Contrasts.NewsCategory
{
	public interface INewsCategoryApplication
	{
		(List<NewsCategoryViewModel>,int) Search(NewsCategorySearchModel searchModel);

        List<NewsCategoryTreeViewModel> GetAllCategories();

		NewsCategoryViewModel FindByCategoryName(string categoryName);

        OperationResult Create(CreateNewsCategory command);
        OperationResult Edit(EditNewsCategory command);
        OperationResult Delete(EditNewsCategory command);

        EditNewsCategory Getdetails(int id);

        void DeleteChild(int id);


    }


}
