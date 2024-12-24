using Westwind.AspNetCore.LiveReload;
using Microsoft.AspNetCore.Authentication.Cookies;
using BusinessLogic.Interfaces;
using BusinessLogic.Managers;
using BusinessLogic.DataAccess;
using BusinessLogic.Strategies;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IEquipmentMediator, EquipmentMediator>();
builder.Services.AddScoped<EquipmentManager>();
builder.Services.AddRazorPages();
builder.Services.AddLiveReload();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Define the login page URL
        options.LoginPath = new PathString("/Login"); 
        options.AccessDeniedPath = new PathString("/AccessDenied");
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IUserMediator, UserMediator>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<EncriptionManager>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ReservationManager>();
builder.Services.AddScoped<AvailabilityManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        if (app.Environment.IsDevelopment())     
        {
            ctx.Context.Response.Headers.Append("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0");
        }
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();

app.Run();
