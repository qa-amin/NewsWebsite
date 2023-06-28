using AccountManagement.Domain.RoleAgg;
using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Domain.RoleClaimAgg
{
    public class RoleClaim : IdentityRoleClaim<long>
    {
        public virtual Role Role { get; set; }
    }
}
