using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsManagement.Application.Contrasts.Tag
{
    public class TagViewModel
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("ردیف")]
        public int Row { get; set; }

        [JsonPropertyName("برچسب"), Display(Name = "عنوان برچسب")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string TagName { get; set; }
    }
}
