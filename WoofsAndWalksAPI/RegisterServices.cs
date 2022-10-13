using Microsoft.EntityFrameworkCore;
using WoofsAndWalksAPI.Data;

namespace WoofsAndWalksAPI;

public static class RegisterServices
{
    public static void ConfigureInjection(this WebApplicationBuilder builder)
    {
        /* Database Context Dependency Injection */
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
       
        // auth

    }
}
