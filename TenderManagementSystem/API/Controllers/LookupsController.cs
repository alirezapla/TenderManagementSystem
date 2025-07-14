using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenderManagementSystem.Application.Queries.Categories;
using TenderManagementSystem.Application.Queries.Status;

namespace TenderManagementSystem.API.Controllers
{
    public class LookupsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public LookupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("categories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _mediator.Send(new GetCategories());
            return Ok(categories);
        }

        [HttpGet("statuses")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStatuses()
        {
            var statuses = await _mediator.Send(new GetStatuses());
            return Ok(statuses);
        }
    }
}