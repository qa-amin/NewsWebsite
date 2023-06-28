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

	}
	
}
