using System.ComponentModel.DataAnnotations;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace Qa.Api.Questions;

[ApiController]
public class QuestionController(IDocumentSession session) : ControllerBase
{
    // /questions
    [HttpGet("/questions")]
    public async Task<ActionResult<IList<QuestionListItem>>> GetAllQuestions()
    {
        var questions = await session.Query<QuestionListItem>().ToListAsync();

        return Ok(questions); // for right now
    }

    [HttpPost("/questions")]
    public async Task<ActionResult> SubmitQuestion(QuestionSubmissionItem question)
    {
        var newQuestion = new QuestionListItem
        {
            Id = Guid.NewGuid(),
            Title = question.Title,
            Content = question.Content,
            SubmittedAnswers = new List<SubmittedAnswer>()
        };
        session.Store(newQuestion);
        await session.SaveChangesAsync();

        return Ok();
    }
}

public record QuestionListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public List<SubmittedAnswer>? SubmittedAnswers { get; set; }
    }

    public record SubmittedAnswer
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
    }

    public record QuestionSubmissionItem
    {
        [MinLength(5), MaxLength(100)] public required string Title { get; set; } = string.Empty;
        [MinLength(10), MaxLength(1000)] public required string Content { get; set; } = string.Empty;
    }