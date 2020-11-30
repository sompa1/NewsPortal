using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(NewsPortal.Web.Areas.Identity.IdentityHostingStartup))]
namespace NewsPortal.Web.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}