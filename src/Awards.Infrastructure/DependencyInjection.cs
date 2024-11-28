
using Asp.Versioning;
using Awards.Domain.Entities;
using Awards.Infraestructure.Data;
using Awards.Infrastructure.Repositories;
using Awards.Service;
using Awards.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Awards.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {

        services.AddTransient<IAwardsService, AwardsService>();
        services.AddTransient<INominateRepository, NominateRepository>();

        services.AddDbContext<AwardsDbContext>(options =>
            options.UseInMemoryDatabase("InMemoryDb"));
 

        AddHealthChecks(services);

        AddApiVersioning(services);

        return services;
    }

    private static void AddHealthChecks(IServiceCollection services)
    {
        services.AddHealthChecks().AddDbContextCheck<AwardsDbContext>();
    }
 
     private static void AddApiVersioning(IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }


}
