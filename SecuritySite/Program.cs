using Microsoft.EntityFrameworkCore;
using SecuritySite.Data;
using SecuritySite.Services;

var builder = WebApplication.CreateBuilder(args);

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
}).AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddTransient<AccountQueryService>();
builder.Services.AddTransient<AccountUpdateService>();
builder.Services.AddSingleton<EmailingService>();
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
