using Microsoft.AspNetCore.Mvc;

namespace ToDoList.APi.Controllers;

public class TodosControllers : ControllerBase
{
    [HttpGet("/todos")]
    public async Task<ActionResult> GetAllTodos()
    {
        return Ok();
    }
}
