using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sessiemeetings.Areas.Identity;
using Sessiemeetings.Data;
using Radzen;
using ClassLibrary;

namespace Sessiemeetings
{

    public class MySignInManager : SignInManager<IdentityUser>
    {

        public MySignInManager(Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager, Microsoft.AspNetCore.Http.IHttpContextAccessor contextAccessor, Microsoft.AspNetCore.Identity.IUserClaimsPrincipalFactory<Microsoft.AspNetCore.Identity.IdentityUser> claimsFactory, Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> optionsAccessor, Microsoft.Extensions.Logging.ILogger<Microsoft.AspNetCore.Identity.SignInManager<Microsoft.AspNetCore.Identity.IdentityUser>> logger, Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider schemes, IUserConfirmation<IdentityUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password,
            bool isPersistent, bool lockoutOnFailure)
        {
            var user = await UserManager.FindByEmailAsync(userName);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddSignInManager<MySignInManager>()   //register new SignInManager 
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Alle karakters toestaan in userName
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789- _@";
            });
                services.AddRazorPages(); 
            services.AddServerSideBlazor();
            services.AddScoped<NotificationService>();
            services.AddScoped<DialogService>();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddScoped<ClassLibrary.Services.AppData>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
