using CasanovaExchange.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
biulder.service.AddLocalization(opt => {opt.ResourcePath = "Resources";})
biulder.service.AddMvc().AddLocalization(LanguageViewLocationExpabderFormat.Suffix).AddDtaAnotationsLocalization();
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
var supportedCultures = new[] {"en","am"};
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultuers[0])
.AddSupportedCultures(supportedCultures)
.AddSupportedUICultures(supportedCultures);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
