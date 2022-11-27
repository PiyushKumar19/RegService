using Microsoft.EntityFrameworkCore;
using RegService.AppDbContext;
using RegService.InterfacesAndSqlRepos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUsersCRUD, SqlUsersCRUDRepo>();
string cs = "Database=RegServiceDb;server=LAPTOP-2SDVC21L;Uid=sa;password=Piyush@1529;";
builder.Services.AddDbContextPool<DatabaseContext>(option =>
option.UseSqlServer(cs));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
