using System.Net;
using System.Net.Http.Json;
using Awards.Domain.Entities;
using Awards.Service.Response;
using AwardsService.IntegrationTests.Infraestructure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AwardsService.IntegrationTests;

public class IntegrationServiceTest : BaseIntegrationTest
{
    private readonly HttpClient _client;

    public IntegrationServiceTest(IntegrationTestWebAppFactory factory)
       : base(factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ApiAcess_MustBe_OK()
    {
        //Arrange

        //Act
        var response = await _client.GetAsync("/api/v1/awards");
        response.EnsureSuccessStatusCode();

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }

    [Fact]
    public async Task ApiAcess_1_Highest_Equal_206()
    {
        //Arrange

        // Act
        var response = await _client.GetAsync("/api/v1/persist");
        response.EnsureSuccessStatusCode();

        var resource = await response.Content.ReadFromJsonAsync<int>();

        //Assert
        Assert.Equal(206, resource);

    }

    [Fact]
    public async Task ApiAcess_2_total_300()
    {
        //Arrange
        var year1 = new Nominate()
        {
            Title = "Movie 1",
            Producers = "Producer from movie 1",
            Studios = "Studios 1",
            Winner = true,
            Year = 1900
        };

        var year2 = new Nominate()
        {
            Title = "Movie 1.2",
            Producers = "Producer from movie 1",
            Studios = "Studios 3",
            Winner = true,
            Year = 2000
        };

        //Act
        var responseInsertYear1 = await _client.PostAsJsonAsync("/api/v1/persist", year1);
        var responseInsertYear2 = await _client.PostAsJsonAsync("/api/v1/persist", year2);


        var responseResult = await _client.GetAsync("/api/v1/awards");
        responseResult.EnsureSuccessStatusCode();


        var resource = await responseResult.Content.ReadFromJsonAsync<Intervals>();

        var id1 = await responseInsertYear1.Content.ReadFromJsonAsync<Guid>();
        var responseResultDelete1 = await _client.DeleteAsync($"/api/v1/persist/{id1}");
        responseResultDelete1.EnsureSuccessStatusCode();

        var id2 = await responseInsertYear2.Content.ReadFromJsonAsync<Guid>();
        var responseResultDelete2 = await _client.DeleteAsync($"/api/v1/persist/{id2}");
        responseResultDelete2.EnsureSuccessStatusCode();

        //Assert
        Assert.Equal(100, resource?.Max.Where(x => x.Producer == "Producer from movie 1").First().interval);
        responseResult.StatusCode.Should().Be(HttpStatusCode.OK);
        responseInsertYear1.StatusCode.Should().Be(HttpStatusCode.Created);
        responseInsertYear2.StatusCode.Should().Be(HttpStatusCode.Created);
        responseResultDelete1.StatusCode.Should().Be(HttpStatusCode.OK);
        responseResultDelete2.StatusCode.Should().Be(HttpStatusCode.OK);


    }


}
