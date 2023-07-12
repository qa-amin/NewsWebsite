using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_NewsManagementQuery.Contracts.CategoryOrTagInfo
{
    public interface ICategoryOrTagInfoQuery
    {
        CategoryOrTagInfoQueryModel FindCategory(int  categoryId);
        CategoryOrTagInfoQueryModel FindTag(long tagId);


    }
}
