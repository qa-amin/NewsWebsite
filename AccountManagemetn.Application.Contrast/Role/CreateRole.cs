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
    public class CreateRole
    {


        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
       
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Description { get; set; }


    }
}
