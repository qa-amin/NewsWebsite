using AccountManagement.Domain.RoleClaimAgg;
using AccountManagement.Domain.UserRoleAgg;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.RoleAgg
{
	public class Role : IdentityRole<long>
	{
		public string Description { get; private set; }

		public DateTime? CreationDate { get; private set; }
		public DateTime? UpdateDate { get; private set; }
		public DateTime? RemoveDate { get; private set; }
		public bool IsRemove { get; private set; }

		public virtual ICollection<UserRole> Users { get; set; }
		public virtual ICollection<RoleClaim> Claims { get; set; }

		public Role()
		{
			CreationDate = DateTime.Now;
			IsRemove = false;
		}

		public void Edit(string name, string description)
		{
			Name = name;
			Description = description;

		}
	}
}
