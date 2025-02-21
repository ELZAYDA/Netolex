using BLL.Repositiries.Implementation;
using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Netolewx.Helperz;
using Netolex.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ?? ??? Connection String ?? appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ?? ????? DbContext ?? SQL Server
builder.Services.AddDbContext<DbApplicationContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddApplicationServices();
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfilies()));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 2;
    options.Password.RequireUppercase = true;

    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<DbApplicationContext>()  // ?? ?? ???????
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/SignIn";       // تحديد مسار صفحة تسجيل الدخول

    options.ExpireTimeSpan = TimeSpan.FromDays(1); // مدة صلاحية الكوكي قبل انتهاء الجلسة
    options.SlidingExpiration = true;  // إعادة ضبط الوقت عند كل طلب جديد

    options.Cookie.HttpOnly = true;    // منع JavaScript من الوصول إلى الكوكي (زيادة الأمان)
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // إجبار استخدام HTTPS فقط
    options.Cookie.SameSite = SameSiteMode.Strict; // منع إرسال الكوكي إلا لنفس الموقع
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();//FILES IN www.root

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
