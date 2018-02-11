using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CIMOBProject.Data;
using CIMOBProject.Models;
using CIMOBProject.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CIMOBProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection=@"Server=(localdb)\mssqllocaldb;Database=CimobPlatform;Trusted_Connection=True;";      

            var connection1 = @"Data Source=SQL6002.site4now.net;Initial Catalog=DB_A2E98B_cimobgroup6;User Id=DB_A2E98B_cimobgroup6_admin;Password=esw4grupo6;";

            var connection2 = @"Data Source=SQL6003.site4now.net;Initial Catalog=DB_A3312B_cimob;User Id=DB_A3312B_cimob_admin;Password=cimob123;";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddDirectoryBrowser();
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })            
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 4;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });

            services.Configure<AuthMessageSenderOptions>(Configuration);


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            app.UseDatabaseErrorPage();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions {
                ServeUnknownFileTypes = true, //allow unkown file types also to be served
                DefaultContentType = "application/vnd.microsoft.portable-executable" //content type to returned if fileType is not known.
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions() {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "install")),
                RequestPath = new PathString("/MyInstall")
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "seriationsDetails",
                    template: "News/Details/Seriacoes",
                    defaults: new { controller = "Applications", action = "DisplaySeriation" });
                routes.MapRoute(
                    name: "seriations",
                    template: "News/Seriacoes",
                    defaults: new { controller = "Applications", action = "DisplaySeriation" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");                
            });
            var serviceScope = app.ApplicationServices.CreateScope();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();            
            DbInitializer.Initialize(context, userManager);            
        }
    }
}
