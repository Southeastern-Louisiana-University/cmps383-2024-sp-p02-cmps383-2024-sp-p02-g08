using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Selu383.SP24.Api.Data;
using Selu383.SP24.Api.Features;

namespace Selu383.SP24.Api.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly DataContext dataContext;
        private readonly RoleManager<Role> roles;

        public AuthenticationController(UserManager<User> userManager, DataContext dataContext, RoleManager<Role> roles)
        {
            this.userManager = userManager;
            this.dataContext = dataContext;
            this.roles = roles;
        }   

        [HttpPost("test")]
        public async Task<IActionResult> IndexAsync()
        {
            User user = new User
            {
                Id = 1,
                Email = "test@gmail.com",
                UserName = "Test"
            };

            var result = await userManager.CreateAsync(user, "Password123");

            await roles.CreateAsync(new Role
            {
                Name = "Admin",
            });

            await userManager.AddToRoleAsync(user, "admin");

            return Ok();
        }


    }
}
