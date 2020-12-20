using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitWeb.DAL;
using VirtusFitWeb.Services;
using AppContext = VirtusFitWeb.DAL.AppContext;
using IProductRepository = VirtusFitWeb.DAL.IProductRepository;
using ProductRepository = VirtusFitWeb.DAL.ProductRepository;
using ProductService = VirtusFitWeb.Services.ProductService;


namespace VirtusFitWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            using var client = new AppContext();
            client.Database.EnsureCreated();
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddRazorRuntimeCompilation();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IDietPlanRepository, DietPlanRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddDbContext<AppContext>(ServiceLifetime.Transient);
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IDietPlanService, DietPlanService>();
            services.AddTransient<IProductInPlanService, ProductInPlanService>();
            services.AddTransient<IFavoriteService, FavoriteService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddSingleton<IBMICalculatorService, BMICalculatorService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddDbContext<AppContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppContext>();

            services.AddHttpClient();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            var columnOptions = new ColumnOptions()
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn("User", SqlDbType.VarChar),
                }
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(connectionString, new MSSqlServerSinkOptions { TableName = "Exceptions", AutoCreateSqlTable = true }, restrictedToMinimumLevel: LogEventLevel.Fatal, columnOptions: columnOptions)
                .Enrich.With<LogEnricher>()
                .CreateLogger();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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
            app.UseSerilogRequestLogging();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            GenerateAdmin(userManager, roleManager).GetAwaiter().GetResult();
        }

        private static async Task GenerateAdmin(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await using var client = new AppContext();

            if (await client.UserRoles.AnyAsync(x =>
                x.RoleId == client.Roles.FirstOrDefault(r => r.Name == "Admin").Id))
            {
                return;
            }

            if (!roleManager.Roles.Any(x => x.Name == "Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var admin = new IdentityUser
            {
                UserName = "admin@admin.ad",
                Email = "admin@admin.ad",
                LockoutEnabled = false
            };

            var result = await userManager.CreateAsync(admin, await File.ReadAllTextAsync("password.txt"));

            var token = await userManager.GenerateEmailConfirmationTokenAsync(admin);
            await userManager.ConfirmEmailAsync(admin, token);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
