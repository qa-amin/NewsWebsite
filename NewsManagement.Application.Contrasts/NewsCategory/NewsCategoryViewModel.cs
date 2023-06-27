using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace NewsManagement.Application.Contrasts.NewsCategory
{
	public class NewsCategoryViewModel
	{
		[JsonPropertyName("Id")]
		public int CategoryId { get; set; }

		[Display(Name = "عنوان دسته بندی"), JsonPropertyName("دسته")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		public string CategoryName { get; set; }

		[JsonPropertyName("ردیف")]
		public int Row { get; set; }

		[Display(Name = "دسته پدر"), JsonPropertyName("دسته پدر")]
		public string? ParentCategoryName { get; set; }


		[Display(Name = "آدرس"), JsonPropertyName("آدرس")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		public string Url { get; set; }
	}
}
