using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using CookieAuthentication.Model;

namespace CookieAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly SignInManager _signInManager;

        public LoginController(IConfiguration config, SignInManager signInManager)
        {
            _signInManager = signInManager;

        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserModel loginUser)
        {
            var resp = await _signInManager.SignIn(HttpContext, loginUser.Email, loginUser.Password);
            if (resp.Success) 
                return Ok(resp);
            
            return Unauthorized();
        }
    }
}