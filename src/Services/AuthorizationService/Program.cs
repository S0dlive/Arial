using AuthorizationService.Data;
using AuthorizationService.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<CreateTestClient>();

builder.Services.AddDbContext<AuthorizationDbContext>(options =>
{
    options.UseMySql("server=localhost;password=;user=root;database=arial.auth;"
        ,ServerVersion.AutoDetect("server=localhost;password=;user=root;database=arial.auth;"));

    options.UseOpenIddict();
});
builder.Services.AddOpenIddict().AddCore(options =>
{
    options.UseEntityFrameworkCore().UseDbContext<AuthorizationDbContext>();
}).AddServer(options =>
{
    options.AllowClientCredentialsFlow();
    
    options.AllowAuthorizationCodeFlow().RequireProofKeyForCodeExchange();
    options.SetAuthorizationEndpointUris("auth/authorize");
    options.SetIntrospectionEndpointUris("auth/introspect");
    options.SetTokenEndpointUris("auth/token");
    options.AddEphemeralEncryptionKey().AddEphemeralSigningKey();
    options.RegisterScopes("api1");
    options.UseAspNetCore().EnableTokenEndpointPassthrough().EnableAuthorizationEndpointPassthrough().DisableTransportSecurityRequirement();      
}).AddValidation(options =>
{ options.UseLocalServer(); options.UseAspNetCore(); });;

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/account/login";
});
builder.Services.AddCors();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseStaticFiles();

app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7262/"));
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();