using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoBalkans.Data.SeedDB
{
    public class SeedData
    {
        public static ApplicationUser AdminUser { get; set; }
        public static ApplicationUser RegularUser { get; set; }

        public static async Task Initialize(MotoBalkansDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //await dbContext.Database.MigrateAsync();

            await SeedRoles(roleManager);

            await SeedUsers(userManager);
        }

        private static async Task SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            // Check if roles exist, create them if they don't
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new ApplicationRole(roleName);
                    var roleGuid = Guid.NewGuid();
                    role.Id = roleGuid.ToString();

                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Check if admin user exists, create it if it doesn't
            AdminUser = await userManager.FindByEmailAsync("admin@acho.com");

            var hasher = new PasswordHasher<ApplicationUser>();

            if (AdminUser == null)
            {

                AdminUser = new ApplicationUser
                {
                    Id = "fa46aee7-58c1-41bb-9dd2-d50ac3dd094b",
                    UserName = "admin@acho.com",
                    NormalizedUserName = "admin@acho.com",
                    Email = "admin@acho.com",
                    NormalizedEmail = "admin@acho.com",
                    FirstName = "Angel",
                    LastName = "Stoykov"
                };

                AdminUser.PasswordHash = hasher.HashPassword(AdminUser, "test123456");
                var result = await userManager.CreateAsync(AdminUser);

                if (result.Succeeded)
                {
                    // Assign the admin role to the admin user
                    await userManager.AddToRoleAsync(AdminUser, "Admin");
                }
                else
                {
                    // Handle error if user creation fails
                    throw new Exception("Failed to create admin user.");
                }
            }

            var regularUser = await userManager.FindByEmailAsync("user@acho.com");
            if (regularUser == null)
            {
                RegularUser = new ApplicationUser
                {
                    Id = "849024e5-a5e7-4db3-90c8-d3448dd22010",
                    UserName = "user@acho.com",
                    NormalizedUserName = "user@acho.com",
                    Email = "user@acho.com",
                    NormalizedEmail = "user@acho.com",
                    FirstName = "Acho",
                    LastName = "RU Lastname"
                };

                RegularUser.PasswordHash = hasher.HashPassword(RegularUser, "test123456");
                var result = await userManager.CreateAsync(RegularUser); // Set the initial password

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(RegularUser, "User");
                }
                else
                {
                    throw new Exception("Failed to create regular user.");
                }
            }
        }
    }
}
