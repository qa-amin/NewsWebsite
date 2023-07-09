using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsManagement.Application.Contrasts.Comment
{
	public class CreateComment
	{
		[JsonPropertyName("نام"), Display(Name = "نام")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		public string Name { get; set; }

		[JsonPropertyName("ایمیل"), Display(Name = "ایمیل")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		[EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد.")]
		public string Email { get; set; }


		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		[JsonPropertyName("دیدگاه"), Display(Name = "دیدگاه")]
		public string Desription { get; set; }

		[JsonIgnore]
		public long NewsId { get; set; }


		[JsonIgnore]
		public long ParentCommentId { get; set; }
	}
}
