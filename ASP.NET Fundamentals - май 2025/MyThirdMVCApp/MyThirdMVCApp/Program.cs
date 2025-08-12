using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyThirdMVCApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 string connectionString = "Server=.;Database=MyThirdMVCAppDb;Trusted_Connection=True;TrustServerCertificate=True";


//NE ZABRAVQI DA SI DOBAVISH USING V DB CONTEXTA :
// using static Common.ApplicationCommonConfiguration; Path-a moje da e razli4en
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
