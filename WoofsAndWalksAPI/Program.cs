using WoofsAndWalksAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Dependency injection services moved to RegisterServices
builder.ConfigureInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthorization();

app.MapControllers();

app.Run();
