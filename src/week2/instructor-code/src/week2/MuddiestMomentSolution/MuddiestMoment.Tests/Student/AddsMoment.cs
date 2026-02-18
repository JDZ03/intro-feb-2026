
using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MuddiestMoment.Api.Student.Endpoints;
using NSubstitute;
using Testcontainers.PostgreSql;

namespace MuddiestMoment.Tests.Student;

public class AddsMoment
{
    [Fact]
    public async Task CanAddAMoment()
    {

        // starting up postgres - notice the version
        var postgreSqlContainer = new PostgreSqlBuilder("postgres:17.5").Build();
        await postgreSqlContainer.StartAsync();

        var stubbedUserProvider = Substitute.For<IProvideUserInformation>();
        stubbedUserProvider.GetUserId().Returns("TEST-USER");
        // Start up my API
        var host = await AlbaHost.For<Program>(config =>
        {
            // example 1 of the "gray box testing" thing.
            config.UseSetting("ConnectionStrings:db-mm", postgreSqlContainer.GetConnectionString());
            // config.ConfigureTestServicesServices
            config.ConfigureServices(sp =>
            {
                sp.AddScoped<IProvideUserInformation>((_) => stubbedUserProvider);
            });
        });
        
 
       

        // Scenario
        // start up the API
        // make a post request with some data to /student/moments
        // the status code should be a 200.
        // We should also get some stuff back.
        // Part 2 later.

        var itemToSend = new StudentMomentCreateModel
        {
            Title = "Containers",
            Description = "Tell me about volumes"
        };

        var response = await host.Scenario(api =>
        {
            // Fluent Interface - a "Domain Specific Language"
            api.Post.Json(itemToSend).ToUrl("/student/moments");
            api.StatusCodeShouldBeOk();

        });

    }
}

/*POST https://localhost:1337/student/moments 
Content-Type: application/json

{
    "title": "Containers",
    "description": "Tell me about volumes"
}

dotnet run // start the api

dotnet test // run my system tests.

*/

/*

           );
 */
