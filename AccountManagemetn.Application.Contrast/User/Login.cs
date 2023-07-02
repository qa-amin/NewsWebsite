using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AccountManagement.Application.Contrast.User
{
	public class Login
	{
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		[Display(Name = "نام کاربری")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		[DataType(DataType.Password)]
		[Display(Name = "کلمه عبور")]
		public string Password { get; set; }

		[Display(Name = "مرا به خاطر بسپار؟")]
		public bool RememberMe { get; set; }
	}
}
