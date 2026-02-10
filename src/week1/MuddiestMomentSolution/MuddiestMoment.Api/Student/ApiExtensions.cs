using MuddiestMoment.Api.Student.Endpoints;
namespace MuddiestMoment.Api.Student;

public static class ApiExtensions
{
    extension(IEndpointRouteBuilder endpoints) 
    { 
        // POST /student/moments
        // GET /student/moments
        public IEndpointRouteBuilder MapStudentEndpoints()
        {
            var group = endpoints.MapGroup("/student/moments");
            // If a post is sent, run this functions
            group.MapPost("", StudentAddsMoment.AddMoment);
            return group;
        }

    }

}
