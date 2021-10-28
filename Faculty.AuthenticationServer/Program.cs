using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Faculty.AuthenticationServer.Tools;
using Faculty.AuthenticationServer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Faculty.AuthenticationServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<CustomUser>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await IdentityInitialize.InitializeAsync(userManager, rolesManager);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
