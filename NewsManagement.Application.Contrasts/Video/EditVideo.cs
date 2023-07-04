using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.Video
{
    public class EditVideo : CreateVideo
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        public string? Poster { get; set; }
    }
}
