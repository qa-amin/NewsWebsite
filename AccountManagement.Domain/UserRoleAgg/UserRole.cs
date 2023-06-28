using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.UserAgg;
using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Domain.UserRoleAgg
{
    public class UserRole : IdentityUserRole<long>
    {
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
