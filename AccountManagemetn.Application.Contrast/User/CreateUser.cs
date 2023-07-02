using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AccountManagement.Domain.UserRoleAgg;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contrast.User
{
	public class CreateUser
	{

		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		[Display(Name = "نام کاربری"), JsonPropertyName("نام کاربری")]
		public string UserName { get; set; }

		[Display(Name = "ایمیل"), JsonPropertyName("ایمیل")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		[EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد.")]
		public string Email { get; set; }

		[Display(Name = "شماره موبایل"), JsonPropertyName("شماره تماس")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		public string PhoneNumber { get; set; }

		[Display(Name = "نام"), JsonPropertyName("نام")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		public string FirstName { get; set; }

		[Display(Name = "نام خانوادگی"), JsonPropertyName("نام خانوادگی")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		public string LastName { get; set; }

		

		[Display(Name = "تاریخ تولد"), JsonPropertyName("تاریخ تولد")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		public string PersianBirthDate { get; set; }

		[Display(Name = "جنسیت"), JsonIgnore]
		[Required(ErrorMessage = "انتخاب {0} الزامی است.")]
		public int Gender { get; set; }


        [JsonIgnore, Display(Name = "نقش")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public string RoleId { get; set; }
       

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        //[StringLength(100, ErrorMessage = "{0} باید دارای حداقل {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password), Display(Name = "کلمه عبور"), JsonIgnore]
		public string Password { get; set; }

		[DataType(DataType.Password), Display(Name = "تکرار کلمه عبور"), JsonIgnore]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		[Compare("Password", ErrorMessage = "کلمه عبور وارد شده با تکرار کلمه عبور مطابقت ندارد.")]
		public string ConfirmPassword { get; set; }

        [JsonIgnore, Display(Name = "تصویر پروفایل")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public IFormFile ImageFile { get; set; }

    }
}
