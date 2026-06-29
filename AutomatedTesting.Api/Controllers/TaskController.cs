using AutomatedTesting.Application.Features.Tasks.Commands.CreateTask;
using AutomatedTesting.Application.Features.Tasks.Commands.DeleteTask;
using AutomatedTesting.Application.Features.Tasks.Commands.UpdateTask;
using AutomatedTesting.Application.Features.Tasks.Queries.GetAllTasks;
using AutomatedTesting.Application.Features.Tasks.Queries.GetTaskById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTesting.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllTasksQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var result = await _mediator.Send(new GetTaskByIdQuery()
        {
            id = id
        });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.IsValidationError) return BadRequest(result);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTaskCommand request)
    {
        request.Id = id;  
        var result = await _mediator.Send(request);

        if (result.IsValidationError) return BadRequest(result);
        if (result.IsNotFound) return NotFound(result);
        if (result.IsError) return StatusCode(500, result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var request = new DeleteTaskCommand()
        {
            Id = id
        };

        var result = await _mediator.Send(request);
        if (result.IsValidationError) return BadRequest(result);
        if (result.IsNotFound) return NotFound(result);
        if (result.IsError) return StatusCode(500, result);
        return Ok(result);
    }
}
