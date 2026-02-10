using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;
namespace MuddiestMoment.Api.Student.Endpoints;

public static class StudentAddsMoment
{
    public static async Task<Ok<StudentMomentResponseModel>> AddMoment(StudentMomentCreateModel request)
    {

        // Get the data sent from the user
        // Make sure they are authenticated
        // We need to validate it 
        // Add that to the db
        // Send a receipt back 
        // this will return an empty 200 Ok status code to the app that called this
        var response = new StudentMomentResponseModel
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            CreatedOn = DateTimeOffset.UtcNow,
            AddedBy = "Fake User"
        };
        // Add it to databse and make sure it saves.
        // tomorrow morning, two liens here will save that in the databse. 
        
        return TypedResults.Ok(response);
    }
}
