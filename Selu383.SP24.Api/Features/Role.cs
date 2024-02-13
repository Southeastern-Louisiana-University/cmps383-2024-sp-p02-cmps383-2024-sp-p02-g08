﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selu383.SP24.Api.Features
{
    public class Role : IdentityRole<int>
    {
        [NotMapped]
        public ICollection<UserRole> Users { get; set; }
    }
}
