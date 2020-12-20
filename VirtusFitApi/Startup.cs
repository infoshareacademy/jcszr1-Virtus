using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using VirtusFitApi.DAL;
using VirtusFitApi.Reports;

namespace VirtusFitApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            using var client = new ApiContext();
            client.Database.EnsureCreated();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApiContext>(ServiceLifetime.Transient);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IProductActionsRepository, ProductActionsRepository>();
            services.AddTransient<IPlanActionsRepository, PlanActionsRepository>();
            services.AddTransient<IReportBuilder, ReportBuilder>();
            services.AddTransient<IUserAccountActionsRepository, UserAccountActionsRepository>();
            services.AddOpenApiDocument(options =>
            {
                options.PostProcess = doc =>
                {
                    doc.Info.Version = "v1";
                    doc.Info.Title = "VirtusFit Reporting API";
                    doc.Info.Description = "Api developed in order to track all actions in VirtusFit diet planner";
                    doc.Info.TermsOfService = "None";
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
