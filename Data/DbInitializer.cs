using Microsoft.AspNetCore.Identity;
using Restaurant.Data.IdentityUpdate;
using Restaurant.Models;

namespace Restaurant.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] rolesNames = { "Admin", "User" };
            foreach(var roleName in rolesNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }

            //Create admin
            var adminEmail = "admin@example.com";
            var userAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (userAdmin == null)
            {
                var newAdmin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, Name = "Admin", EmailConfirmed = true };
                var result = await userManager.CreateAsync(newAdmin, "Admin123!");

                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    }
}
