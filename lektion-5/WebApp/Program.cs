using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Contexts
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));


// External Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddFacebook(x =>
{
    x.ClientId = builder.Configuration["Facebook:ClientId"]!;
    x.ClientSecret = builder.Configuration["Facebook:ClientSecret"]!;
})
.AddGoogle(x =>
{
    x.ClientId = builder.Configuration["Google:ClientId"]!;
    x.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
})
.AddMicrosoftAccount(x =>
{
    x.ClientId = builder.Configuration["MicrosoftAccount:ClientId"]!;
    x.ClientSecret = builder.Configuration["MicrosoftAccount:ClientSecret"]!;
});
//.AddTwitter(x =>
//{
//    x.ConsumerKey = builder.Configuration["Twitter:ConsumerKey"]!;
//    x.ConsumerSecret = builder.Configuration["Twitter:ConsumerSecret"]!;
//})

// Identity
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DataContext>();


var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
