using CasanovaExchange.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CasanovaExchange;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICommodityRepository, CommodityExchangeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddLocalization(option => options.ResourcesPath = "Resources");
builder.Services.AddRazorPages()
    .AddViewLocalization(LanguageViewLocalizationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.AddDbContextPool<CommodityExchangeContext>(
    options => options.UseSqlServer(
           builder.Configuration.GetConnectionString("" +
           "CommodityConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().
    AddEntityFrameworkStores<CommodityExchangeContext>();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Account/Signin";
});

var app = builder.Build();
var supportedCultures = new[] { "en", "fr" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// this is where the defualt admin is created 

defualtLogin _defualtLogin = new defualtLogin();
 _defualtLogin.defualt();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
