using CasanovaExchange.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
builder.Services.AddMvc().AddLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDtaAnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(
    opt =>
    {
        var supporetdCultures = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("am")
        };
        opt.DefaultRequestCulture = new RequestCulture("en");
        opt.SupportedCultures = supporetdCultures;
        opt.SupportedUICultures = supporetdCultures;
    }
    );
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICommodityRepository, CommodityExchangeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



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
app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
//var supportedCultures = new[] {"en","am"};
//var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultuers[0])
//.AddSupportedCultures(supportedCultures)
//.AddSupportedUICultures(supportedCultures);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
