using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_NewsManagementQuery.Contracts.NewsPaginate;
using NewsManagement.Domain.NewsAgg;

namespace _1_NewsManagementQuery.Query
{
	public class NewsPaginateQuery : INewsPaginateQuery
	{
		private readonly  INewsRepository _newsRepository;

		public NewsPaginateQuery(INewsRepository newsRepository)
		{
			_newsRepository = newsRepository;
		}

		public NewsPaginateQueryModel GetNewsPaginate(int limit, int offset)
		{
			var countNewsPublished = _newsRepository.CountNewsPublished();
			var news = _newsRepository.GetPaginateNews(offset, limit, "PublishDateTime desc", "", true, null);

			var newsPaginateQueryModel = new NewsPaginateQueryModel(countNewsPublished, news);
			return newsPaginateQueryModel;
		}
	}
}
