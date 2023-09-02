using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SecuritySite.Data;
using SecuritySite.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "User Id=postgres;Host=localhost;Port=8081;Database=Voltic;Password=_Gentile12;Pooling=true; Maximum Pool Size=1024;" ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddRazorPages();

//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")!);
//});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql("User Id=postgres;Host=localhost;Port=8081;Database=Voltic;Password=_Gentile12;Pooling=true; Maximum Pool Size=1024;");
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

    options.LoginPath = new PathString("/Account/Login");
    options.AccessDeniedPath = new PathString("/Account/Logout");
    options.AccessDeniedPath = new PathString("/Account/AccessDenied");

    options.SlidingExpiration = true;
    // etc :)
});

builder.Services.AddTransient<AccountQueryService>();
builder.Services.AddTransient<AccountUpdateService>();
builder.Services.AddSingleton<EmailingService>();
//builder.Services.AddTransient<IEmailSender , EmailingService>();
var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
