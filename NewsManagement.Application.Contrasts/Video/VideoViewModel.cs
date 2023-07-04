using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsManagement.Application.Contrasts.Video
{
    public class VideoViewModel
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("ردیف")]
        public int Row { get; set; }

        [JsonPropertyName("عنوان ویدیو"), Display(Name = "عنوان ویدیو")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Title { get; set; }

        [Display(Name = "آدرس ویدیو"), Url(ErrorMessage = "آدرس وارد شده نا معتبر است.")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Url { get; set; }

        public string Poster { get; set; }

        [JsonIgnore]
        public DateTime? PublishDateTime { get; set; }

        [JsonPropertyName("تاریخ انتشار")]
        public string PersianPublishDateTime { get; set; }
    }
}
