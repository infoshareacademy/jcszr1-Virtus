using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddDbContext<AppContext>(ServiceLifetime.Transient);
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IDietPlanService, DietPlanService>();
            services.AddTransient<IProductInPlanService, ProductInPlanService>();
            services.AddTransient<IFavoriteService, FavoriteService>();
            services.AddSingleton<IBMICalculatorService, BMICalculatorService>();
            services.AddDbContext<AppContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppContext>();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
