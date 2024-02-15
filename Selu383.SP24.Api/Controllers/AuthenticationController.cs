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

        [HttpPost("/api/authentication/login")]
        public async Task<ActionResult> LoginAsync([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (!result.Succeeded)
            {
                return BadRequest("Invalid username or password");
            }

            var roles = await userManager.GetRolesAsync(user);
            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = roles.ToArray()
            };

            return Ok(userDto);
        }

        [HttpPost("/api/authentication/logout")]
        public async Task<ActionResult> LogoutAsync()
        {
            await signInManager.SignOutAsync();

            return Ok("Logged out successfully");
        }


        [HttpGet("secret")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetSecret()
        {
            return Ok("secret");
        }

        [HttpGet("/api/authentication/me")]
        public async Task<ActionResult<UserDto>> GetAuthenticatedUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userName = User.Identity.Name;

            var user = await userManager.FindByNameAsync(userName);
            var roles = await userManager.GetRolesAsync(user);

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = roles.ToArray()
            };

            return Ok(userDto);
        }

    }
}
