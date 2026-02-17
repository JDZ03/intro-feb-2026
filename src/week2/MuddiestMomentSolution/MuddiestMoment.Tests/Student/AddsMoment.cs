using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MuddiestMoment.Api.Student.Endpoints;
using NSubstitute;
using System.Diagnostics;
using Testcontainers.PostgreSql;

namespace MuddiestMoment.Tests.Student;

public class AddsMoment
{
    [Fact]
    public async Task CanAddMoment()
    {
        var postgreSqlContainer = new PostgreSqlBuilder("postgres:17.5").Build();
        await postgreSqlContainer.StartAsync();
        var stubbedUserProvider = Substitute.For<IProvideUserInformation>();
        stubbedUserProvider.GetUserId().Returns("TEST-USER")

        var host = await AlbaHost.For<Program>(config =>
        {
            config.UseSetting("ConnectionStringsLdb-mm", postgreSqlContainer.GetConnectionString());
            config.ConfigureTestServices(sp =>
            {
                sp.AddScoped<IProvideUserInformation>((_) => stubbedUserProvider)
            });
        });
        // Scenario
        // Start up the API
        // make a post request with some data to /student/moments
        // the status code should be a 200
        // We should also get some stuff back.

        var itemToSend = new StudentMomentCreateModel { Title = "Containers", Description = "Tell me about volumes" };
        var response = await host.Scenario(api =>
        {
            api.Post.Json(itemToSend).ToUrl("/student/moments");
            api.StatusCodeShouldBeOk();
        });
    }
}
