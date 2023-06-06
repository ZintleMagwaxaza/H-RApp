
using Microsoft.AspNetCore.Identity;
using Display1.Data.CustomProvider;
using Display1.Models;
using Microsoft.EntityFrameworkCore;
using Display1.CustomProvider;
using Display1.Data;
using Display1.Service;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace Display1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMvc();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            IServiceCollection serviceCollection = builder.Services.AddDbContext<AdventureWorks2019Context>(options =>
                options.UseSqlServer(connectionString));
            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddScoped<SignInManager<ApplicationUser>>();

            // Configure Identity
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
               .AddRoles<ApplicationRole>()
               .AddRoleManager<CustomRoleManager>()
               .AddUserManager<CustomUserManager>()
              .AddUserStore<CustomUserStore>()
              //.AddRoleStore<CustomRoleStore>()
              .AddEntityFrameworkStores<AdventureWorks2019Context>()
              .AddDefaultTokenProviders();

            // .AddSignInManager();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });

            builder.Services.AddHttpContextAccessor();


            builder.Services.AddLogging(builder =>
            {
                builder.AddFilter("Microsoft.AspNetCore.Authorization", LogLevel.Debug);
                builder.AddConsole();
            });


            // Add other services
            builder.Services.AddScoped<SearchService>();
            builder.Services.AddScoped<JobCandidateService>();
            builder.Services.AddScoped<EmployeeService>();

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddTransient<ApplicationUsersTable>();
            builder.Services.AddScoped<IUserStore<ApplicationUser>, CustomUserStore>();
            builder.Services.AddScoped<IRoleStore<ApplicationRole>, CustomRoleStore>();
            builder.Services.AddScoped<IUserRoleStore<ApplicationUser>, CustomUserStore>();
            builder.Services.AddScoped<RoleManager<ApplicationRole>>();
            builder.Services.AddScoped<CustomRoleManager>();
            builder.Services.AddBlazorise().AddBootstrapProviders().AddFontAwesomeIcons();
            builder.Services.AddScoped<UserManager<ApplicationUser>>();

            // builder.Services.AddScoped<RoleManager>();

            builder.Services.AddScoped<WeatherForecastService>();

            // Build the application
            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
              
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

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");




            app.Run();
        }
    }
}





