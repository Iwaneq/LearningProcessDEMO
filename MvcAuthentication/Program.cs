global using MvcAuthentication.Core.Data;
global using Microsoft.EntityFrameworkCore;
using MvcAuthentication.Core.Services;
using MvcAuthentication.Core.Services.Identity;
using MvcAuthentication.Core.Services.Level;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
{
    options.Cookie.Name = "CookieAuth";
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", 
        policy => policy.RequireClaim("Role", "Admin"));
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("MvcAuthentication.Core"));
});

builder.Services.AddScoped<DrawQuestionService>();
builder.Services.AddScoped<AccountAccessService>();
builder.Services.AddScoped<LevelProgressStateAccessService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<LoginService>();


builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().WithRazorPagesRoot("/Views");

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
