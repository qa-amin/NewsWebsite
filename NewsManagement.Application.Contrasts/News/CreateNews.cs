using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace NewsManagement.Application.Contrasts.News
{
    public class CreateNews
    {
        [Display(Name = "عنوان خبر")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است."), Display(Name = "چکیده")]
        public string Abstract { get; set; }


        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "آدرس خبر")]
        public string Url { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "متن خبر")]
        public string Description { get; set; }

        public bool FuturePublish { get; set; }

        [Display(Name = "تاریخ انتشار"), JsonPropertyName("تاریخ انتشار")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string? PersianPublishDate { get; set; }


        [Display(Name = "زمان انتشار")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string? PersianPublishTime { get; set; }



        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        [Display(Name = "نوع خبر")]
        public bool IsInternal { get; set; }



        public NewsNewsCategoryViewModel? NewsNewsCategoryViewModel { get; set; }

        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        [ Display(Name = "برچسب ها")]
        public string NameOfTags { get; set; }


        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        [JsonIgnore, Display(Name = "تصویر شاخص")]
        public string ImageFile { get; set; }

        public string? ImageName { get; set; }

        public bool IsPublish { get; set; }
        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        [Display(Name = "دسته بندی")]
        public int[] CategoryIds { get; set; }
        public string SubmitButton { get; set; }

        public long UserId { get; set; }
        public DateTime? PublishDateTime { get; set; }
        

    }

}

   
