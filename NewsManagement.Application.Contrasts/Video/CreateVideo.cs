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
    public class CreateVideo
    {
        


        [JsonPropertyName("عنوان ویدیو"), Display(Name = "عنوان ویدیو")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Title { get; set; }

        [Display(Name = "آدرس ویدیو"), Url(ErrorMessage = "آدرس وارد شده نا معتبر است.")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Url { get; set; }

        [Display(Name = "پوستر ویدیو"), JsonIgnore]
        //[Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public IFormFile PosterFile { get; set; }

       
    }
}
