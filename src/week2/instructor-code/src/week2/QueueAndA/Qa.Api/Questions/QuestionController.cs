using System.ComponentModel.DataAnnotations;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace Qa.Api.Questions;

// enables "auto validation" of incoming request bodies.
// must be there to get the OpenApi stuff generated.
[ApiController]
public class QuestionController(IDocumentSession session) : ControllerBase
{

    [HttpGet("/questions/{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var question = await session.Query<QuestionListItem>().Where(q => q.Id == id)
            .SingleOrDefaultAsync();

        if (question is null)
        {
            return NotFound();
        }
        return Ok(question);
    }
   
    // /questions
    [HttpGet("/questions")]

    public async Task<ActionResult<IList<QuestionListItem>>> GetAllQuestions()
    {

        // DO NOT EVER NEVER EVER DO THIS THIS IS A DEMO
        var questions = await session.Query<QuestionListItem>().ToListAsync();
        await Task.Delay(3000); // wait three seconds before sending this response.

        return Ok(questions); // for right now
    }

    [HttpPost("/questions")]
    public async Task<ActionResult> SubmitQuestion(QuestionSubmissionItem question)
    {

        var newQuestion = new QuestionListItem
        {
            Id = Guid.NewGuid(),
            Title = question.Title,
            Question = question.Question,
            SubmittedAnswers = new List<SubmittedAnswer>()
        };
        session.Store(newQuestion);
        await session.SaveChangesAsync();

        return Ok();
    }
}

public record QuestionListItem
{
    public required Guid Id { get; set; }
    [MinLength(5), MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MinLength(30), MaxLength(500)]
    public required string Question { get; set; } = string.Empty;

    public List<SubmittedAnswer>? SubmittedAnswers { get; set; }
}

public record SubmittedAnswer
{
    public Guid Id { get; set; }
    public string Question { get; set; } = string.Empty;

}

public record QuestionSubmissionItem
{
    [MinLength(5), MaxLength(50)] public required string Title { get; set; } = string.Empty;
    [MinLength(30), MaxLength(500)] public required string Question { get; set; } = string.Empty;

    public required int Priority { get; set; }
}