using Microsoft.EntityFrameworkCore;
using CloudApp.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        
        context.Database.EnsureCreated();

       
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new CloudApp.Models.User
                {
                    Nickname = "Ilona",
                    Status = "Learning AWS Cloud",
                    BirthDate = new DateTime(1993, 3, 12),
                    RegistrationDate = DateTime.UtcNow.AddDays(-10),
                    LastLogin = DateTime.UtcNow
                },
                new CloudApp.Models.User
                {
                    Nickname = "CloudDev",
                    Status = "Active",
                    BirthDate = new DateTime(1995, 5, 20),
                    RegistrationDate = DateTime.UtcNow.AddDays(-5),
                    LastLogin = DateTime.UtcNow.AddHours(-2)
                },
                new CloudApp.Models.User
                {
                    Nickname = "TudorFan",
                    Status = "Offline",
                    BirthDate = new DateTime(1992, 10, 5),
                    RegistrationDate = DateTime.UtcNow.AddDays(-3),
                    LastLogin = DateTime.UtcNow.AddDays(-1)
                }
            );
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при подготовке или заполнении базы данных.");
    }
}


app.Run();