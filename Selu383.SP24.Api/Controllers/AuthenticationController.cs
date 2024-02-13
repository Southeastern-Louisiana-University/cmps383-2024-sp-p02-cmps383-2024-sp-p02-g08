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

        public AuthenticationController(UserManager<User> userManager, DataContext dataContext)
        {
            this.userManager = userManager;
            this.dataContext = dataContext;
        }   

        [HttpPost("test")]
        public async Task<IActionResult> IndexAsync()
        {
            var result = await userManager.CreateAsync(new User
            {
                Email = "foo@gmail.com",
                UserName = "foo"
            }, "Password123");
            return Ok();
        }
    }
}
