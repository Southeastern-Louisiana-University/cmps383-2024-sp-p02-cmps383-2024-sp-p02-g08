using Microsoft.AspNetCore.Identity;

namespace Selu383.SP24.Api.Features
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> Users { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
