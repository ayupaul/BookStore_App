using DataAccessLayer.Models;
using BuisnessLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLayer;
using FluentValidation;
using BuisnessLayer.Data.Repository.IServices;
using BuisnessLayer.Data.Repository.Services;
using BuisnessLayer.Repository.Services;
using BuisnessLayer.UnitOfWork;
using BuisnessLayer.Repository;
using BuisnessLayer.Facade;
using BuisnessLayer.Factory;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using BookStore.NotifyHub;

namespace BookStore
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
            services.AddSingleton<Microsoft.Extensions.Logging.ILoggerFactory, LoggerFactory>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
                options.AddInterceptors(new MyDbCommandInterceptor());
            });


            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddRoleManager<RoleManager<IdentityRole>>();

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddControllersWithViews();

            services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IBookEventService, BookEventService>();
            services.AddScoped<IAdminServices, AdminServices>();
            //services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookReadingEventFacade, BookReadingEventFacade>();
            services.AddScoped<IBookReadingEventFactory, BookReadingEventFactory>();
            services.AddTransient<IValidator<SignUpViewModel>, SignUpViewModelValidator>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)

        {
            SeedData(userManager, roleManager).Wait();
            if (env.IsDevelopment())

            {
                Console.WriteLine("Test message");
                app.UseDeveloperExceptionPage();

            }

            else

            {

                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();

            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<NotificationHub>("/notificationsHub");
            });
        }
        private async Task SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create the admin role if it doesn't exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Create the admin user if it doesn't exist
            var adminUserName = "Admin";
            if (await userManager.FindByNameAsync(adminUserName) == null)
            {
                var adminUser = new IdentityUser { UserName = adminUserName };
                var result = await userManager.CreateAsync(adminUser, "Admin123@");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }







    }
}






