using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Selu383.SP24.Api.Data;
using Selu383.SP24.Api.Features;
using Selu383.SP24.Api.Features.Hotels;

namespace Selu383.SP24.Api.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly DataContext dataContext;
        private readonly RoleManager<Role> roles;
        private readonly SignInManager<User> signInManager;

        public AuthenticationController(UserManager<User> userManager, 
                                        DataContext dataContext, 
                                        RoleManager<Role> roles,
                                        SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.dataContext = dataContext;
            this.roles = roles;
            this.signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid username or password");
            }

            var roles = await userManager.GetRolesAsync(user);
            var userDto = new UserDto
            {
                UserName = user.UserName,
                Roles = roles.ToArray()
            };

            return Ok(userDto);
        }

        [HttpGet("secret")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetSecret()
        {
            return Ok("secret");
        }


        [HttpPost("test")]
        public async Task<IActionResult> IndexAsync()
        {
            User user1 = new User
            {
                Email = "test@gmail.com",
                UserName = "galkadi"
            };

            var result = await userManager.CreateAsync(user1, "Password123!");

            await roles.CreateAsync(new Role
            {
                Name = "Admin",
            });

            await roles.CreateAsync(new Role
            {
                Name = "User",
            });

            await userManager.AddToRoleAsync(user1, "Admin");

            return Ok();
        }


    }
}
