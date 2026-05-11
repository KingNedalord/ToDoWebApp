using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;

var builder = WebApplication.CreateBuilder();

builder.Services.AddControllersWithViews();
builder.Services
    .AddDbContext<ToDoContext>(options => options
        .UseSqlite("Data Source=todo.db"));

var app = builder.Build();

app.UseRouting();

app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();