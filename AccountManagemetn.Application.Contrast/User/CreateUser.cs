using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AccountManagement.Domain.UserRoleAgg;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contrast.User
{
	public class CreateUser
	{

		[JsonPropertyName("ردیف")]
		public int Row { get; set; }

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

		[Display(Name = "تاریخ تولد")]
		public DateTime? BirthDate { get; set; }

		[Display(Name = "تاریخ تولد")]
		[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		public string PersianBirthDate { get; set; }


		[Display(Name = "تاریخ عضویت"), System.Text.Json.Serialization.JsonIgnore]
		public DateTime? RegisterDateTime { get; set; }

		[Display(Name = "تاریخ عضویت")]
		public string PersianRegisterDateTime { get; set; }

		[Display(Name = "فعال / غیرفعال")]
		public bool IsActive { get; set; }

		[Display(Name = "جنسیت")]
		public int Gender { get; set; }

		[Display(Name = "نقش")]
		public string RoleName { get; set; }

        [JsonIgnore, Display(Name = "نقش")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public string RoleId { get; set; }
        [JsonIgnore]
        public ICollection<UserRole> Roles { get; set; }
        [Display(Name = "رمز")]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز")]
		[Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [JsonIgnore, Display(Name = "تصویر پروفایل")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public IFormFile ImageFile { get; set; }

    }
}
