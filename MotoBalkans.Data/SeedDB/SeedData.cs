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
        public static async Task Initialize(MotoBalkansDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            await dbContext.Database.MigrateAsync();

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
                    await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }
        }

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Check if admin user exists, create it if it doesn't
            var adminUser = await userManager.FindByEmailAsync("admin@acho.com");

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@acho.com",
                    FirstName = "Angel",
                    LastName = "Stoykov"
                };

                var result = await userManager.CreateAsync(adminUser, "test123456"); // Set the initial password

                if (result.Succeeded)
                {
                    // Assign the admin role to the admin user
                    await userManager.AddToRoleAsync(adminUser, "Admin");
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
                regularUser = new ApplicationUser
                {
                    UserName = "user",
                    Email = "user@acho.com",
                    FirstName = "Angel - user",
                    LastName = "Stoykov - user"
                };

                var result = await userManager.CreateAsync(regularUser, "test123456"); // Set the initial password

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, "User");
                }
                else
                {
                    throw new Exception("Failed to create regular user.");
                }
            }
        }
    }
}
