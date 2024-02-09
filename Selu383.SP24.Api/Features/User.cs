using Microsoft.AspNetCore.Identity;

namespace Selu383.SP24.Api.Features
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> Roles { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
