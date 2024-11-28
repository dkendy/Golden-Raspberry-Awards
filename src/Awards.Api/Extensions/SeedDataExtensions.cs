using System.Data;
using System.Globalization; 
using Awards.Domain.Entities;
using Awards.Infraestructure.Data;
using CsvHelper.Configuration;
using Serilog;


namespace Awards.Api.Extensions;

internal static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AwardsDbContext>();
            context.Database.EnsureCreated();
             

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var caminhoArquivo = Path.Combine(basePath, "Resources", "movielist.csv");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                MissingFieldFound = null,
                HasHeaderRecord = true
            };


            using var reader = new StreamReader(caminhoArquivo);
            using var csv = new CsvHelper.CsvReader(reader, config);

            var nominates = new List<Nominate>();

            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var registro = new Nominate
                {
                    Year = csv.GetField<int>(0),
                    Title = csv.GetField<string>(1),
                    Studios = csv.GetField<string>(2),
                    Producers = csv.GetField<string>(3),
                    Winner = csv.GetField<string>(4) == "yes"
                };
                nominates.Add(registro);
            }

            context.AddRange(nominates);
            context.SaveChanges();

            Log.Information($"Total importado: {context.Nominates.Count()}");
        }
    }
}
