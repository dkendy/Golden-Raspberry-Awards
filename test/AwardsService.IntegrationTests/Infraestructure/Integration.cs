using Xunit;
using Awards.Infraestructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AwardsService.IntegrationTests.Infraestructure;

 
public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope; 
    protected readonly AwardsDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
 
        DbContext = _scope.ServiceProvider.GetRequiredService<AwardsDbContext>();
    }
}

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<DbContext>));

            services.AddDbContext<AwardsDbContext>(options =>
            options.UseInMemoryDatabase("InMemoryDb")); 
 
        });
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return Task.CompletedTask;
    }
}