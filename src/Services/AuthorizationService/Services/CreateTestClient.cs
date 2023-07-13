using AuthorizationService.Data;
using OpenIddict.Abstractions;

namespace AuthorizationService.Services;

public class CreateTestClient : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public CreateTestClient(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AuthorizationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("postman", cancellationToken) is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "postman",
                ClientSecret = "postman-secret",
                DisplayName = "Postman",
                RedirectUris = {new Uri("https://oauth.pstmn.io/v1/callback")},
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.ResponseTypes.Code,
                    OpenIddictConstants.Permissions.ResponseTypes.Token,

                    OpenIddictConstants.Permissions.Prefixes.Scope + "resource_server_1"
                }
            }, cancellationToken);
        }
        if (await manager.FindByClientIdAsync("course_service") is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "course_service",
                ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Introspection
                }
            });
        }
        
        var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();
        
        if (await scopeManager.FindByNameAsync("course_service") is null)
        {
            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                DisplayName = "course",
                
                Name = "course_service",
                Resources =
                {
                    "course_service"
                },
                Description = "allow this client to : create, update, delete and like other course."
            });
        }
    }
}