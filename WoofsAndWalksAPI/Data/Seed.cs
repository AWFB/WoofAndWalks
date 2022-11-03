using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
        if (users == null) return;
        
        // Create User roles
        var roles = new List<AppRole>()
        {
            new AppRole { Name = "Member" },
            new AppRole { Name = "Admin" },
            new AppRole { Name = "Moderator" }
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        // generate passwords for seed users
        foreach (var user in users)
        {
            user.UserName = user.UserName.ToLower();
            await userManager.CreateAsync(user, "Password1!");
            await userManager.AddToRoleAsync(user, "member");
        }
        
        // Create admin user (also moderator)
        var admin = new AppUser
        {
            UserName = "admin"
        };
        
        await userManager.CreateAsync(admin, "Password1!");
        await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
    }
}
