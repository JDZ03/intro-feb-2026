using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;
namespace MuddiestMoment.Api.Student.Endpoints;

public static class StudentAddsMoment
{
    public static async Task<Ok<StudentMomentResponseModel>> AddMoment(StudentMomentCreateModel request, IDocumentSession session)
    {

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
        var entity = new StudentMomentEntity
        {
            // mapping
            Id = response.Id,
            Title = response.Title,
            Description = response.Description,
            AddedBy = response.AddedBy,
            CreatedOn = response.CreatedOn
        };

        // will vary depending on what library/database you are using
        session.Store(entity);
        await session.SaveChangesAsync();
        
        return TypedResults.Ok(response);
    }
}
