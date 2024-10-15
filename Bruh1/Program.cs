using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProviderLayer;
using ProviderLayer.Processors;
using System.Text;
namespace MVC
{
    public class Program  
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container.
            builder.Services.AddAuthentication("CookieAuth")
            .AddCookie("CookieAuth", options =>
            {
                options.Cookie.Name = "UserLoginCookie";
                options.LoginPath = "/Account/Login";
                //options.LogoutPath = "/Account/Logout";  
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            })
            .AddJwtBearer("JWTAuth",options =>
            {
                var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthorization();
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();
            app.UseSession();

            app.MapControllerRoute(
                name: "area",
                pattern: "{area:exists}/{controller}/{action}/{id?}");

            app.MapControllerRoute(
                name: "NoArea",
                pattern: "{controller}/{action}/{id?}");


			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
        }
    }
}
