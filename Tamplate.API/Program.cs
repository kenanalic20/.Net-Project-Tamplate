using Microsoft.EntityFrameworkCore;
using Tamplate.Infrastructure.Extensions;
using Tamplate.Domain.Data;
using Tamplate.Infrastructure.Seeders;
using Tamplate.API.Extensions;
using Tamplate.Application.Extensions;
using Tamplate.API.Middleware;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();
//For Development of File Manager Dashboard
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendCors", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5500",
                "http://127.0.0.1:5500")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Tamplate.Domain")));

builder.Services.AddAutoMapperService();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddSeeders();


builder.Services.AddAuthService(builder.Configuration);
var app = builder.Build();

app.UseStaticFiles();
app.UseMiddleware<ExceptionHandlingMiddleware>();
//For Development of File Manager Dashboard
app.UseCors("FrontendCors");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tamplate API v1");
    });
}


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    for (int i = 0; i < 30; i++)
    {
        try
        {
            context.Database.Migrate();
            break;
        }
        catch (Exception ex)
        {
            Thread.Sleep(5000);
        }
    }

    var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
    await roleSeeder.SeedRolesAsync();

    var userSeeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
    await userSeeder.SeedUsersAsync();

}

// File Manager Dashboard Route
app.MapGet("/file-manager", async context =>
{
    var htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "file-manager", "index.html");
    if (File.Exists(htmlPath))
    {
        var html = await File.ReadAllTextAsync(htmlPath);
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync(html);
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("File Manager Dashboard not found. Please create wwwroot/file-manager/index.html");
    }
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
