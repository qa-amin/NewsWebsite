using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AccountManagement.Domain.UserClaimAgg;
using AccountManagement.Domain.UserRoleAgg;
using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Domain.UserAgg
{
	public class User : IdentityUser<long>
	{
		public string FirstName { get;private set; }
		public string LastName { get; private set; }
		public DateTime? BirthDate { get;  private set; }
		public string Image { get; private set; }
		public DateTime? RegisterDateTime { get;  private set; }
		public bool IsActive { get;  private set; }
		public int Gender { get; private set; } 

		public DateTime? UpdateDate { get; private set; }
		public DateTime? RemoveDate { get; private set; }
		public bool IsRemove { get; private set; }

		public virtual ICollection<UserRole> Roles { get; set; }
		public virtual ICollection<UserClaim> Claims { get; set; }

        public User(string userName, string email, string phoneNumber ,string firstName, string lastName, DateTime? birthDate, string image, int gender)
        {
			UserName = userName;
			Email = email;
			PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Image = image;
            Gender = gender;
            RegisterDateTime = DateTime.Now;
            IsRemove = false;
            IsActive = true;
			EmailConfirmed = false;
			
        }
        public void Edit(string userName, string email, string phoneNumber, string firstName, string lastName, DateTime? birthDate, string image, int gender)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            if (!string.IsNullOrWhiteSpace(image))
                Image = image;
            Gender = gender;
            RegisterDateTime = DateTime.Now;
            IsRemove = false;
            IsActive = true;
            EmailConfirmed = false;
            UpdateDate = DateTime.Now;

        }

        public void Delete()
        {
            IsRemove = true;
            RemoveDate = DateTime.Now;
        }

        public void Active()
        {
            IsActive = true;
            UpdateDate = DateTime.Now;
        }
        public void DeActive()
        {
            IsActive = false;
            UpdateDate = DateTime.Now;
        }
    }
	
}
