using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.Tag
{
    public class TagSearchModel
    {
        public string? Search { get; set; }
        public string? Order { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string? Sort { get; set; }
    }
}
