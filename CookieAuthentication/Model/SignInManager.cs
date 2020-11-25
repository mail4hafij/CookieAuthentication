using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace CookieAuthentication.Model
{
    public class SignInManager
    {
        private readonly IConfiguration _config;

        public SignInManager(IConfiguration config)
        {
            _config = config;
        }

        public async Task<SignInResult> SignIn(HttpContext httpContext, string email, string password)
        {
            // Do the email verification here.
            // Assuming the the email verification is done and we 
            // have the following loggedInUser.
            UserModel loggedInUser = new UserModel
            {
                Email = "test@test.com",
                Role = RoleModel.Client,
                UserId = 123456789
            };

            var identity = new ClaimsIdentity(GetClaimsCookieAuthentication(loggedInUser), CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return new SignInResult(loggedInUser, true);
        }

        public async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        private IEnumerable<Claim> GetClaimsCookieAuthentication(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "" + user.UserId),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email),
            };
            return claims;
        }
    }
}
