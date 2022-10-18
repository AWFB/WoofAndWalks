using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Data;

public class Seed
{
    public static async Task SeedUsers(IApplicationBuilder app)
    {
        DataContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<DataContext>();

        //if (await context.Users.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

        // generate passwords for seed users
        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();
            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password1!"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);
            await context.Database.MigrateAsync();
            await context.SaveChangesAsync();
        }
        
    }
}
