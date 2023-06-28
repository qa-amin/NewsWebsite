using AccountManagement.Domain.UserAgg;
using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Domain.UserClaimAgg
{
    public class UserClaim : IdentityUserClaim<long>
    {
        public virtual User User { get; set; }
    }
}
