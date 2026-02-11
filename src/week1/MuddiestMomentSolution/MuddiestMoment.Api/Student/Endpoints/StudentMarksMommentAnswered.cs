using Marten;

namespace MuddiestMoment.Api.Student.Endpoints;

public static class StudentMarksMommentAnswered
{
    public static async Task<IResult> MarkQuestionAnswered(Guid id, IDocumentSession session)
    {
        var savedMoment = await session.Query<StudentMomentEntity>()
            .Where(m => m.Id == id)
            .SingleOrDefaultAsync();

        if (savedMoment is null)
        {
            return TypedResults.Ok();
        }
        savedMoment.isAnswered = true;
        session.Store(savedMoment);
        await session.SaveChangesAsync();
        return TypedResults.Ok();
    }
}
