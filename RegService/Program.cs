using Microsoft.AspNetCore.Identity;
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
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LoginByFormNumber}/{id?}");

app.Run();
