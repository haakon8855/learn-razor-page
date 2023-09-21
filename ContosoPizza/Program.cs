using ContosoPizza.Data;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PizzaContext>(options =>
    options.UseSqlite("Data Source=ContosoPizza.db"));
builder.Services.AddScoped<PizzaService>();
builder.Services.AddScoped<SauceService>();
builder.Services.AddScoped<ToppingService>();
builder.Services.AddScoped<SodaService>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews().AddViewLocalization();

var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("no"),
    new CultureInfo("en"),
    new CultureInfo("de"),
    new CultureInfo("it"),
    new CultureInfo("es"),
    new CultureInfo("fr"),
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider { Options = options });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var requestLocalizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (requestLocalizationOptions != null)
{
    app.UseRequestLocalization(requestLocalizationOptions.Value);
}

app.Use(async (context, next) =>
{
    if (requestLocalizationOptions == null)
    {
        await next();
        return;
    }
    var cookieProvider = requestLocalizationOptions.Value.RequestCultureProviders
        .OfType<CookieRequestCultureProvider>()
        .FirstOrDefault();

    if (cookieProvider != null)
    {
        if (context.Request.Cookies.TryGetValue("PreferredLanguage", out var cookieValue))
        {
            var cookieCulture = new CultureInfo(cookieValue);

            Thread.CurrentThread.CurrentCulture = cookieCulture;
            Thread.CurrentThread.CurrentUICulture = cookieCulture;
        }
    }
    await next();
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
