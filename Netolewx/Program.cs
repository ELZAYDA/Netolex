using BLL.Repositiries.Implementation;
using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Netolewx.Helperz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ?? ??? Connection String ?? appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ?? ????? DbContext ?? SQL Server
builder.Services.AddDbContext<DbApplicationContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieRepo, MovieRepo>();
builder.Services.AddScoped<IGenreRepo, GenreRepo>();
builder.Services.AddScoped<IDirectorRepo, DirectorRepo>();
builder.Services.AddScoped<IActorRepo, ActorRepo>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfilies()));


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

app.UseAuthorization();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
