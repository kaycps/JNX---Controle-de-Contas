using System;
using ControleFaturamentoJnx.Areas.Identity.Data;
using ControleFaturamentoJnx.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;

[assembly: HostingStartup(typeof(ControleFaturamentoJnx.Areas.Identity.IdentityHostingStartup))]
namespace ControleFaturamentoJnx.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseMySql("Server=localhost;Database=controle_faturamento_jnx;User=root;Password=160494;",
                mySqlOptions => mySqlOptions.ServerVersion(new ServerVersion(new Version(8, 0, 3), ServerType.MySql))));

                services.AddDefaultIdentity<ApplicationUser>(options => 
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<AuthDbContext>();
            });
        }
    }
}