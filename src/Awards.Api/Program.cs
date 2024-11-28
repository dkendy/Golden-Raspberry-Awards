

using Asp.Versioning.ApiExplorer;
using Awards.Api.Extensions;
using Awards.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
   loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
 
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure();

WebApplication app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    foreach (ApiVersionDescription description in app.DescribeApiVersions())
    {
        string url = $"/swagger/{description.GroupName}/swagger.json";
        string name = description.GroupName.ToUpperInvariant();
        options.SwaggerEndpoint(url, name);
    }
});
 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.SeedData();

app.MapHealthChecks("/health");

app.Run();

public partial class Program;

