using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenderManagementSystem.Application.Commands.Tenders;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Application.Queries.Tenders;
using TenderManagementSystem.Core.Models;

namespace TenderManagementSystem.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class TendersController : ControllerBase
{
    private readonly IMediator _mediator;

    public TendersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetTendersQuery
        {
            QueryParams = new QueryParams
            {
                PaginationParams = new PaginationParams { Number = pageNumber, Size = pageSize }
            }

        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(string id)
    {
        var tender = await _mediator.Send(new GetTenderByIdQuery {TenderId = id});
        if (tender == null) return NotFound();
        return Ok(tender);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateTenderDTO model)
    {
        var command = new UpdateTenderCommand() {Id = id, Tender = model};
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteTenderCommand {Id = id});
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTenderDto model)
    {
        var command = new CreateTenderCommand {Tender = model};
        var result = await _mediator.Send(command);
        return Ok(new {TenderId = result});
    }
}