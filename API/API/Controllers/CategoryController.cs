using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.CQRS.Commands;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CategoryAddCommand addCategory)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CategoryAddCommand { Name = addCategory.Name };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var command = new CategoryGetCommand();
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();
        return Ok(result);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var command = new CategoryGetByIdCommand { Id = id };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CategoryUpdateCommand updateCategory)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CategoryUpdateCommand { Id = updateCategory.Id, Name = updateCategory.Name };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new CategoryDeleteCommand { Id = id };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();
        return Ok(result);
    }
}
