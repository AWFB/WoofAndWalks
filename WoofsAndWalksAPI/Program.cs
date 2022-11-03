using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WoofsAndWalksAPI;
using WoofsAndWalksAPI.Data;
using WoofsAndWalksAPI.Middleware;
using WoofsAndWalksAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Dependency injection services moved to RegisterServices
builder.ConfigureInjection();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    //SeedData.EnsureDataPopulated(app);
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(userManager, roleManager);
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
