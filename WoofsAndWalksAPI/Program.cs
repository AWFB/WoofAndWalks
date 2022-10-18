using WoofsAndWalksAPI;
using WoofsAndWalksAPI.Data;
using WoofsAndWalksAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Dependency injection services moved to RegisterServices
builder.ConfigureInjection();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    //SeedData.EnsureDataPopulated(app);
    Seed.SeedUsers(app);
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
