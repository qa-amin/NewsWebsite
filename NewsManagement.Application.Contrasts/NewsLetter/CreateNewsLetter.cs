using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsManagement.Application.Contrasts.NewsLetter
{
	public class CreateNewsLetter
	{
		[JsonPropertyName("Id"), Display(Name = "ایمیل")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است."), EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد.")]
		public string Email { get; set; }
	}
}
