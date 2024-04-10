using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GearClub.Data;
using GearClub.Areas.Identity.Data;
using Microsoft.Extensions.Options;
using GearClub.Repositories;
using GearClub.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GearClubContextConnection") ?? throw new InvalidOperationException("Connection string 'GearClubContextConnection' not found.");

builder.Services.AddDbContext<GearClubContext>(options => options.UseSqlServer(connectionString));

// Add Identity services and configurations
/*builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<GearClubContext>();*/
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<GearClubContext>()
        .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{    
    options.User.RequireUniqueEmail = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;    
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Views/Shared/AccessDeniedView";
});

// Add Repositories 
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepository<Image>, ImageRepository>();
builder.Services.AddScoped<IRepository<Category_Product>, CategoryProductRepository>();
builder.Services.AddScoped<IRepository<Cart>, CartRepository>();
builder.Services.AddScoped<IRepository<CartDetail>, CartDetailRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IRepository<Specification>, SpecificationRepo>();
//builder.Services.AddScoped<IRepository<OrderDetail>, OrderDetailRepo>();

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
