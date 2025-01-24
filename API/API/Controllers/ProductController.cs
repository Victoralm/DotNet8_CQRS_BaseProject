using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.CQRS.Commands;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductAddCommand addProduct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new ProductAddCommand { Name = addProduct.Name, Description = addProduct.Description, CategoryId = addProduct.CategoryId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var command = new ProductGetCommand();
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var command = new ProductGetByIdCommand { Id = id };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ProductUpdateCommand updateCategory)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new ProductUpdateCommand { Id = updateCategory.Id, Name = updateCategory.Name };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new ProductDeleteCommand { Id = id };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();
        return Ok(result);
    }
}
