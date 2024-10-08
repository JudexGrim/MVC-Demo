using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVC.Attributes;
using ProviderLayer.Processors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels;

namespace MVC.Controllers
{

    public class AuthController : BaseController
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttemptLogin(LoginViewModel loginInfo, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                using AuthenticationProcessor authenticationProcessor = new AuthenticationProcessor();
                var result = await authenticationProcessor.AttemptLogin(loginInfo);

                if (result.success)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        SetCookie(result.userModel.Username);

                        returnUrl = GetBaseURl() + returnUrl;

                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return createresponse(false, "Login Info is Invalid");
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WowSuperSecret"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(30)
            
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task SetCookie(string username)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30), 
                HttpOnly = true,                       
            };

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));
        }
    }
}
