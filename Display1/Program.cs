using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Display1.CustomProvider;
using Display1.Data;
using Display1.Data.CustomProvider;
using Display1.Models;
using Display1.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace WebApp
{
    public class Program
    {

        public static void Main(string[] args)
        {
          CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public class Startup
        {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            public void ConfigureServices(IServiceCollection services)
            {
               // services.AddMvc();
                services.AddDbContext<AdventureWorks2019Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);

                     // Configure Identity
                      services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                         .AddRoles<ApplicationRole>()
                         .AddRoleManager<CustomRoleManager>()
                         .AddUserManager<CustomUserManager>()
                        .AddUserStore<CustomUserStore>()
                        //.AddRoleStore<CustomRoleStore>()
                        .AddEntityFrameworkStores<AdventureWorks2019Context>()
                        .AddDefaultTokenProviders();
                      // .AddSignInManager();  */




                  services.AddAuthorization(options =>
                   {
                       options.AddPolicy("AdminOnly", policy =>
                       {
                           policy.RequireRole("Admin");
                       });
                   });

                   services.AddHttpContextAccessor();
                   services.AddLogging(builder =>
                   {
                       builder.AddFilter("Microsoft.AspNetCore.Authorization", LogLevel.Debug);
                       builder.AddConsole();
                   });

                //Search service
                //services.AddDatabaseDeveloperPageExceptionFilter();
                services.AddScoped<SearchService>();
                services.AddSingleton<JobCandidateService>();
                services.AddScoped<EmployeeService>();


                services.AddRazorPages();
                services.AddServerSideBlazor();
                services.AddSingleton<WeatherForecastService>();
                services.AddTransient<ApplicationUsersTable>();
                 services.AddScoped<SignInManager<ApplicationUser>>();
                services.AddScoped<IUserStore<ApplicationUser>, CustomUserStore>();
                services.AddScoped<IRoleStore<ApplicationRole>, CustomRoleStore>();
                services.AddScoped<IUserRoleStore<ApplicationUser>, CustomUserStore>();
                services.AddScoped<RoleManager<ApplicationRole>>();
                services.AddScoped<CustomRoleManager>();
                services.AddBlazorise().AddBootstrapProviders().AddFontAwesomeIcons();


            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
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

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");
                });
            }
        }
    }
}
