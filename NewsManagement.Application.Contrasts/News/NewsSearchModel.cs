using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.News
{
    public class NewsSearchModel
    {
        public string? Search { get; set; }
        public string? Order { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string? Sort { get; set; }
    }
}
