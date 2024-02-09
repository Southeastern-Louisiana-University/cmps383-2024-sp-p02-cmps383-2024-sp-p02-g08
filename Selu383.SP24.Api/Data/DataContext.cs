using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Selu383.SP24.Api.Features;

namespace Selu383.SP24.Api.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

 
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

         Seed();
        }

        private async Task Seed()
        {
          
            var roleManager = this.GetService<RoleManager<Role>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new Role { Name = "Admin" });
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new Role { Name = "User" });
            }

            var userManager = this.GetService<UserManager<User>>();

            if (await userManager.FindByNameAsync("galkadi") == null)
            {
                var user = new User { UserName = "galkadi" };
                await userManager.CreateAsync(user, "Password123!");
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (await userManager.FindByNameAsync("bob") == null)
            {
                var user = new User { UserName = "bob" };
                await userManager.CreateAsync(user, "Password123!");
                await userManager.AddToRoleAsync(user, "User");
            }

            if (await userManager.FindByNameAsync("sue") == null)
            {
                var user = new User { UserName = "sue" };
                await userManager.CreateAsync(user, "Password123!");
                await userManager.AddToRoleAsync(user, "User");
            }
        }
    }
}