using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_NewsManagementQuery.Contracts.CategoryOrTagInfo
{
    public class CategoryOrTagInfoQueryModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsCategory { get; set; }
    }
}
