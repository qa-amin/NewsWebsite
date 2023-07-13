using NewsManagement.Application.Contrasts.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagement.Domain.UserAgg;

namespace _1_NewsManagementQuery.Contracts.UserBookMark
{
    public class UserBookMarkQueryModel
    {
        public UserBookMarkQueryModel(User user, List<NewsViewModel> bookmarks)
        {
            User = user;
            Bookmarks = bookmarks;
        }
        public User User { get; set; }
        public List<NewsViewModel> Bookmarks { get; set; }
    }
}
