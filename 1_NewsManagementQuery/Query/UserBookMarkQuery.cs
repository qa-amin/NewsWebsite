using _1_NewsManagementQuery.Contracts.UserBookMark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsManagement.Domain.NewsAgg;

namespace _1_NewsManagementQuery.Query
{
    public class UserBookMarkQuery : IUserBookMarkQuery
    {
        private readonly INewsRepository _newsRepository;

        public UserBookMarkQuery(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }


        public UserBookMarkQueryModel GetBookMark(long userId)
        {
            var bookMarkNews = _newsRepository.GetBookMark(userId);
            return new UserBookMarkQueryModel(null, bookMarkNews);
        }
    }


}
