using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_NewsManagementQuery.Contracts.UserBookMark
{
    public interface IUserBookMarkQuery
    {
        UserBookMarkQueryModel GetBookMark(long userId);
    }
}
