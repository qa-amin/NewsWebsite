using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsWebsite.Entities;

namespace NewsManagement.Domain.NewsCategoryAgg
{
	public interface INewsCategoryRepository : IRepository<int,NewsCategory>
	{
		NewsCategoryViewModel FindByCategoryName(string categoryName);
		List<NewsCategoryTreeViewModel> GetAllCategories();

		(List<NewsCategoryViewModel>, int) Search(NewsCategorySearchModel searchModel);

		int CountNewsCategory();

		EditNewsCategory GetDetails(int id);

        List<NewsCategory> findChildCategories(int id);


    }
}
