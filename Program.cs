using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();
builder.Services
    .AddDbContext<ToDoContext>(options => options
        .UseSqlite("Data Source=todo.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // This generates the raw JSON spec at /swagger/v1/swagger.json
    app.UseSwagger();

    // This serves the visual browser UI at /swagger
    app.UseSwaggerUI();
}

app.UseRouting();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}")
    .WithStaticAssets();

app.Run();