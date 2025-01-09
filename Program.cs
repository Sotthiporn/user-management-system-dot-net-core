using Microsoft.EntityFrameworkCore;
using user_management_dot_net_core.Configs;
using user_management_dot_net_core.Database;
using user_management_dot_net_core.Database.Seeders;
using user_management_dot_net_core.Helpers;
using user_management_dot_net_core.Middlewares;
using user_management_dot_net_core.Repositories;
using user_management_dot_net_core.Repositories.Interfaces;
using user_management_dot_net_core.Services;
using user_management_dot_net_core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register Helpers
builder.Services.AddSingleton<TokenHelper>();

// Database configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Register JWT authentication configuration
JwtConfig.AddJwtAuthentication(builder.Services, builder.Configuration);

// Register controllers and endpoints
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register Swagger configuration
SwaggerConfig.AddSwagger(builder.Services);

// Build Application
var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await UserSeeder.SeedAsync(context);
}

// Use Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Register Middlewares
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<PermissionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
