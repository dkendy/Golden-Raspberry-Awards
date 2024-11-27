using System;
using System.Globalization;
using AwardsService.DTOs;
using AwardsService.Entities;
using CsvHelper.Configuration;

namespace AwardsService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<AwardsDbContext>());

    }

    private static void SeedData(AwardsDbContext context)
    {
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

        Console.WriteLine($"Total importado: {context.Nominates.Count()}");
    }
}