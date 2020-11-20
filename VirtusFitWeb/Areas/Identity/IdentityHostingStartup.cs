using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(VirtusFitWeb.Areas.Identity.IdentityHostingStartup))]
namespace VirtusFitWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}