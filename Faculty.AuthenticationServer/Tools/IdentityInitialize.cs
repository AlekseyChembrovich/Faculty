using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Faculty.AuthenticationServer.Models;

namespace Faculty.AuthenticationServer.Tools
{
    /// <summary>
    /// Identity initialize for set up default value.
    /// </summary>
    public class IdentityInitialize
    {
        /// <summary>
        /// Method for init admin user.
        /// </summary>
        /// <param name="userManager">User manager identity.</param>
        /// <param name="roleManager">Role manager identity.</param>
        /// <returns>An instance of the Task class.</returns>
        public static async Task InitializeAsync(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string login = "Admin12345";
            const string password = "Admin12345";
            if (await roleManager.FindByNameAsync("administrator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("administrator"));
            }

            if (await roleManager.FindByNameAsync("employee") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("employee"));
            }

            if (await userManager.FindByNameAsync(login) == null)
            {
                var admin = new CustomUser { UserName = login, Birthday = DateTime.Now };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(admin, new[] { "administrator", "employee" });
                }
            }
        }
    }
}
