using System;
using Bakery.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Bakery.Areas.Identity.IdentityHostingStartup))]
namespace Bakery.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BakeryContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BakeryContextConnection")));

               
            });
        }
    }
}