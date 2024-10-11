using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;
using MVC.Attributes;
using ProviderLayer.Processors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels;

namespace MVC.Controllers
{

    public class AccountController : BaseController
    {
        private IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
           _configuration = configuration;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttemptLogin(LoginViewModel loginInfo, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                using AuthenticationProcessor authenticationProcessor = new AuthenticationProcessor();
                var result = await authenticationProcessor.TryLogin(loginInfo);

                if (result.success)
                {
                    var token = GenerateJwtToken(result.userModel);
                    await SetCookie(result.userModel, token);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {

                        returnUrl = GetBaseURl() + returnUrl;

                        return Redirect(returnUrl);
                    }

                    else return RedirectToAction("Index", "Home");

                }

                else return Json(new { success = false });
                
            }
            else return PartialView("~/Views/Home/LoginFailed.cshtml");
        }

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var handler = new JwtSecurityTokenHandler();

            var claims = new Claim[]
            {
                new Claim("ID", user.ID.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim("User Email", user.Email),
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        private async Task SetCookie(User? userModel, string jwtToken)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30), 
                HttpOnly = true,            
                Secure = true,
            };

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, userModel.Email),
                new Claim("ID", userModel.ID.ToString()),
                new Claim("Email", userModel.Email),
                new Claim("JWT Token", jwtToken)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));
        }
    }
}
