using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GearClub.Data;
using GearClub.Areas.Identity.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GearClubContextConnection") ?? throw new InvalidOperationException("Connection string 'GearClubContextConnection' not found.");

builder.Services.AddDbContext<GearClubContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<GearClubContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
