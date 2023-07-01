using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AccountManagement.Application.Contrast.Role
{
    public class RoleViewModel
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("ردیف")]
        public int Row { get; set; }

        [Display(Name = "عنوان نقش"), JsonPropertyName("عنوان نقش")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "توضیحات"), JsonPropertyName("توضیحات")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Description { get; set; }

        [JsonPropertyName("تعداد کاربران")]
        public long UsersCount { get; set; }

    }
}
