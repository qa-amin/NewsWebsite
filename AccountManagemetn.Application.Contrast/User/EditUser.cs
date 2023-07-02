using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using AccountManagement.Domain.UserRoleAgg;

namespace AccountManagement.Application.Contrast.User
{
	public class EditUser : CreateUser
    {
        [JsonPropertyName("Id")]
		public long Id { get; set; }
        [Display(Name = "تصویر پروفایل"), JsonPropertyName("تصویر پروفایل")]
        public string Image { get; set; }
        public ICollection<UserRole> Roles { get; set; }

        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }
	}
}
