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
            var connection=@"Server=(localdb)\mssqllocaldb;Database=CimobProject;Trusted_Connection=True;";

            


            var connection1 = @"Data Source=SQL6002.site4now.net;Initial Catalog=DB_A2E98B_cimobgroup6;User Id=DB_A2E98B_cimobgroup6_admin;Password=esw4grupo6;";


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }*/
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            app.UseDatabaseErrorPage();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            /*using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                // Seed the database.
                if (context.Roles.SingleOrDefault(r => r.Name == "Student") == null)
                {

                    context.Roles.Add(new IdentityRole { Name = "Student", NormalizedName = "Student" });
                    context.SaveChanges();
                }
                
                if (context.Roles.SingleOrDefault(r => r.Name == "Employee") == null)
                {
                    context.Roles.Add(new IdentityRole { Name = "Employee", NormalizedName = "Employee" });
                    context.SaveChanges();
                }

                if (!context.Employees.Any()) {
                    var user = new Employee {
                        UserName = "testemployee@cimob.pt",
                        UserFullname = "Empregado Teste",
                        Email = "testemployee@cimob.pt",
                        UserCc = 123456789,
                        PhoneNumber = "936936936",
                        UserAddress = "RuaTeste",
                        PostalCode = "2900-000",
                        BirthDate = new DateTime(1996, 1, 1),
                        EmployeeNumber = 150221055
                    };
                    userManager.CreateAsync(user, "teste12").Wait();
                    var role = context.Roles.SingleOrDefault(m => m.Name == "Employee");
                    userManager.AddToRoleAsync(user, role.Name).Wait();
                    context.SaveChanges();
                }

                if (!context.Colleges.Any()) {
                    context.Colleges.Add(new College { CollegeAlias = "ESTS", CollegeName = "Escola Superior de Tecnologia de Setúbal" });
                    context.Colleges.Add(new College { CollegeAlias = "ESCE", CollegeName = "Escola Superior de Ciências Empresariais" });
                    context.Colleges.Add(new College { CollegeAlias = "ESE", CollegeName = "Escola Superior de Educação" });
                    context.Colleges.Add(new College { CollegeAlias = "ESTB", CollegeName = "Escola Superior de Tecnologia do Barreiro" });
                    context.SaveChanges();
                }

                if (!context.CollegeSubjects.Any()) {
                    context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "EI", SubjectName = "Engenharia Informática", CollegeId = 1 });
                    context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "EM", SubjectName = "Engenharia Mecânica", CollegeId = 1 });
                    context.SaveChanges();
                }
            }*/
            //DbInitializer.Initialize(context);
            
        }
    }
}
