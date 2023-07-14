using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.NumberOfVisitChart
{
    public class NumberOfVisitChartViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("y")]
        public long Value { get; set; }
    }
}
