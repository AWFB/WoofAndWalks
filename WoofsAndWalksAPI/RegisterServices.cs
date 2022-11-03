using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WoofsAndWalksAPI.Data;
using WoofsAndWalksAPI.Data.Repositories;
using WoofsAndWalksAPI.Helpers;
using WoofsAndWalksAPI.Interfaces;
using WoofsAndWalksAPI.Models;
using WoofsAndWalksAPI.Services;

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
        builder.Services.AddCors();
        builder.Services.AddScoped<LogUserActivity>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ILikesRepository, LikesRepository>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

        // auth and JWT- AddIdentityCore needed for token auth 
        builder.Services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<AppRole>()
            //.AddRoleManager<RoleManager<AppRole>>() ?No longer needed
            .AddSignInManager<SignInManager<AppUser>>()
            //.AddRoleValidator<RoleValidator<AppRole>>() ?No longer needed
            .AddEntityFrameworkStores<DataContext>();

        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])), 
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        
        // Auth policies
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            options.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
        });

        // Cloudinary
        builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
        builder.Services.AddScoped<IPhotoService, PhotoService>();

    }
}
