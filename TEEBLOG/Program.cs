using Core.DbContext;
using Core.Models;
using Logic.Helper;
using Logic.IHelper;
using Logic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services helpers
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<IAdminHelper, AdminHelper>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailHelper, EmailHelper>();

builder.Services.AddSingleton<IEmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

// Add services to the container.
builder.Services.AddDbContext<AppDBContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("FirstTableTB")));

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<AppDBContext>();

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
    pattern: "{controller=Base}/{action=Index}/{id?}");
UpdateDatabase(app);

app.Run();

static void UpdateDatabase(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope())
    {
        using (var context = serviceScope.ServiceProvider.GetService<AppDBContext>())
        {
            context?.Database.Migrate();
        }
    }
}