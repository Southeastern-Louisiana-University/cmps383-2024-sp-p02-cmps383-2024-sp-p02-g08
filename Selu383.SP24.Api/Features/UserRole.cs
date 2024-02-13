using Microsoft.AspNetCore.Identity;

namespace Selu383.SP24.Api.Features
{
    public class UserRole : IdentityUserRole<int>

    {
        public Role Role { get; set; }
        public User User { get; set; }

    }
}
