using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Data;
using Selu383.SP24.Api.Features;
using Selu383.SP24.Api.Features.Hotels;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<DataContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    await db.Database.MigrateAsync();

    var hotels = db.Set<Hotel>();

    if (!await hotels.AnyAsync())
    {
        for (int i = 0; i < 6; i++)
        {
            db.Set<Hotel>()
                .Add(new Hotel
                {
                    Name = "Hammond " + i,
                    Address = "1234 Place st",
                }) ;
        }

        await db.SaveChangesAsync();
    }

    var users = db.Set<User>();

    if (!await users.AnyAsync())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

        User user1 = new User
        {
            Email = "test@gmail.com",
            UserName = "galkadi"
        };

        User user2 = new User
        {
            Email = "test1@gmail.com",
            UserName = "bob"
        };

        User user3 = new User
        {
            Email = "test2@gmail.com",
            UserName = "sue"
        };

        await userManager.CreateAsync(user1, "Password123!");
        await userManager.CreateAsync(user2, "Password123!");
        await userManager.CreateAsync(user3, "Password123!");


        await roleManager.CreateAsync(new Role
        {
            Name = "Admin"
        });


        await roleManager.CreateAsync(new Role
        {
            Name = "User"
        });

        await userManager.AddToRoleAsync(user1, "Admin");
        await userManager.AddToRoleAsync(user2, "User");
        await userManager.AddToRoleAsync(user3, "User");

        await db.SaveChangesAsync();
    }

}

app.UseHttpsRedirection();

app.UseAuthorization();

app
    .UseRouting()
    .UseEndpoints(x =>
    {
        x.MapControllers();
    });

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSpa(x =>
    {
        x.UseProxyToSpaDevelopmentServer("http://localhost:5173");
    });
}
else
{
    app.MapFallbackToFile("/index.html");
}

app.Run();

//see: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0
// Hi 383 - this is added so we can test our web project automatically
public partial class Program { }
