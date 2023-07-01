using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AccountManagement.Domain.UserRoleAgg;

namespace AccountManagement.Application.Contrast.User
{
	public class UserViewModel
	{
		[JsonPropertyName("Id")]
		public long? Id { get; set; }

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

		[Display(Name = "تاریخ تولد"),JsonIgnore]
		public DateTime? BirthDate { get; set; }

        [Display(Name = "تاریخ تولد"), JsonPropertyName("تاریخ تولد")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
		public string PersianBirthDate { get; set; }

		[Display(Name = "تصویر پروفایل"), JsonPropertyName("تصویر پروفایل")]
		public string Image { get; set; }

		[Display(Name = "تاریخ عضویت"),JsonIgnore]
		public DateTime? RegisterDateTime { get; set; }

        [Display(Name = "تاریخ عضویت"), JsonPropertyName("تاریخ عضویت")]
        public string PersianRegisterDateTime { get; set; }

        [Display(Name = "فعال / غیرفعال"), JsonPropertyName("IsActive")]
        public bool IsActive { get; set; }

		[Display(Name = "جنسیت"), JsonPropertyName("جنسیت")]
		public string Gender { get; set; }

        [JsonPropertyName("نقش")]
        public string RoleName { get; set; }

        [System.Text.Json.Serialization.JsonIgnore, Display(Name = "نقش")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public long? RoleId { get; set; }
        [Display(Name = "معرفی"), JsonPropertyName("معرفی")]
        public string Bio { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<UserRole> Roles { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
		public bool PhoneNumberConfirmed { get; set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public bool TwoFactorEnabled { get; set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public bool LockoutEnabled { get; set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public bool EmailConfirmed { get; set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public int AccessFailedCount { get; set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public DateTimeOffset? LockoutEnd { get; set; }
	}
}
