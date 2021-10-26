using System;
using DigiMarketWebApp.Areas.Identity.Data;
using DigiMarketWebApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DigiMarketWebApp.Areas.Identity.IdentityHostingStartup))]
namespace DigiMarketWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DigiMarketDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DigiMarketDbContextConnection")));

                services.AddDefaultIdentity<WebAppUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireLowercase = false;
                })
                    .AddEntityFrameworkStores<DigiMarketDbContext>();
            });
        }
    }
}