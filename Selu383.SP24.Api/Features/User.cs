using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selu383.SP24.Api.Features
{
    public class User : IdentityUser<int>
    {
        [NotMapped]
        public ICollection<UserRole> Roles { get; set; }

    }
}

