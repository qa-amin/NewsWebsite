using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using NewsManagement.Application.Contrasts.NewsLetter;
using NewsManagement.Domain.NewsAgg;

namespace NewsManagement.Domain.NewsLetterAgg
{
	public interface INewsLetterRepository : IRepository<string,NewsLetter>
	{
		NewsLetterViewModel GetNewsLetterWithEmail(string email);

		(List<NewsLetterViewModel>, int) Search(NewsLetterSearchModel searchModel);

		List<NewsLetterViewModel> GetPaginateNewsletter(int offset, int limit, string orderBy, string searchText);
	}
}
