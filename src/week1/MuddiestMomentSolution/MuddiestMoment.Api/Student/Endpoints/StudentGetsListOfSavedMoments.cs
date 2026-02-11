using Marten;

namespace MuddiestMoment.Api.Student.Endpoints;

public static class StudentGetsListOfSavedMoments
{
    public static async Task<IResult> GetAllMomentsForStudent(IDocumentSession session)
    {
        var moments = await session.Query<StudentMomentEntity>()
            .Where(m => m.AddedBy == "fake user" && m.isAnswered == false)
            .Select(m => new StudentMomentResponseModel
            {
                Id = m.Id,
                AddedBy = m.AddedBy,
                CreatedOn = m.CreatedOn,
                Description = m.Description,
                Title = m.Title
            }
            )
            .ToListAsync();

        return TypedResults.Ok(moments);
    }
}
