using OpenIddict.Validation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenIddict().AddValidation(options =>
{
    options.AddAudiences("course_service");
    options.UseIntrospection();
    options.SetIssuer("https://localhost:7262/");
    options.SetClientId("course_service");
    options.SetClientSecret("846B62D0-DEF9-4215-A99D-86E6B8DAB342");
    options.UseSystemNetHttp();
    options.UseAspNetCore();
});

builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();