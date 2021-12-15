using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Faculty.AuthenticationServer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Faculty.AuthenticationServer.Tools
{
    public static class PreparingDatabase
    {
        public static void PrepSeedData(IApplicationBuilder app, bool isProd)
        {
            CustomIdentityContext context = default;
            ILogger<Startup> logger = default;
            UserManager<CustomUser> userManager = default;
            RoleManager<IdentityRole> roleManager = default;
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                context = serviceScope.ServiceProvider.GetRequiredService<CustomIdentityContext>();
                logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
                userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();
                roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                SeedData(context, logger, userManager, roleManager, isProd);
            }
        }

        private static void SeedData(CustomIdentityContext context, ILogger<Startup> logger, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, bool isProd)
        {
            bool isApplied = ApplyMigration(isProd, logger, context);
            if (isApplied)
            {
                if (!roleManager.Roles.Any())
                {
                    SeedRoles(roleManager);
                }

                if (!userManager.Users.Any())
                {
                    SeedUsers(userManager);
                }
            }
            else
            {
                logger.LogWarning("Data did not seed in the database.");
            }
        }

        private static bool ApplyMigration(bool isProd, ILogger<Startup> logger, CustomIdentityContext context)
        {
            if (isProd)
            {
                logger.LogInformation("--> Attempting to apply migration...");
                try
                {
                    context.Database.Migrate();
                    logger.LogInformation("--> Migration applied.");
                    return true;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }

            return false;
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync("administrator") == null)
            {
                roleManager.CreateAsync(new IdentityRole("administrator")).Wait();
            }

            if (roleManager.FindByNameAsync("employee") == null)
            {
                roleManager.CreateAsync(new IdentityRole("employee")).Wait();
            }
        }

        private static void SeedUsers(UserManager<CustomUser> userManager)
        {
            const string login = "Admin12345";
            const string password = "Admin12345";
            if (userManager.FindByNameAsync(login) == null)
            {
                var admin = new CustomUser { UserName = login, Birthday = DateTime.Now };
                var result = userManager.CreateAsync(admin, password);
                if (result.Result.Succeeded)
                {
                    userManager.AddToRolesAsync(admin, new[] { "administrator", "employee" }).Wait();
                }
            }
        }
    }
}
