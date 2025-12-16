using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Shouldly;

namespace TextAdventureTests.Api;

public class ApiDocumentationAvailabilityTests
{
    [Fact]
    public async Task Documentation_IsAvailable_ByDefaultInDevelopment()
    {
        using var factory = CreateFactory("Development");
        using var client = factory.CreateClient();

        var openApiResponse = await client.GetAsync("/openapi/v1.json");
        openApiResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var scalarResponse = await client.GetAsync("/scalar/v1");
        scalarResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Documentation_IsDisabled_ByDefaultInProduction()
    {
        using var factory = CreateFactory("Production");
        using var client = factory.CreateClient();

        var openApiResponse = await client.GetAsync("/openapi/v1.json");
        openApiResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        var scalarResponse = await client.GetAsync("/scalar/v1");
        scalarResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Documentation_CanBeEnabled_InProductionWithConfiguration()
    {
        var configuration = new Dictionary<string, string?>
        {
            ["ApiDocumentation:Enabled"] = "true",
            ["ApiDocumentation:RequireAuthorization"] = "false"
        };

        using var factory = CreateFactory("Production", configuration);
        using var client = factory.CreateClient();

        var openApiResponse = await client.GetAsync("/openapi/v1.json");
        openApiResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var scalarResponse = await client.GetAsync("/scalar/v1");
        scalarResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    private static WebApplicationFactory<Program> CreateFactory(
        string environment,
        IDictionary<string, string?>? configurationOverrides = null)
    {
        return new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment(environment);

            if (configurationOverrides is not null)
            {
                builder.ConfigureAppConfiguration((_, configBuilder) =>
                {
                    configBuilder.AddInMemoryCollection(configurationOverrides);
                });
            }
        });
    }
}
