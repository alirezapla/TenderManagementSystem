using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenderManagementSystem.Application.Commands.Bids;
using TenderManagementSystem.Application.DTOs;

namespace TenderManagementSystem.API.Controllers
{
    [Route("[controller]")]
    public class BidsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BidsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBid([FromBody] CreateBidDto model)
        {
            var command = new CreateBidCommand { Bid = model };
            var bidId = await _mediator.Send(command);
            return Ok(new {id = bidId});
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateBidStatus(string id, [FromBody] UpdateBidDto model)
        {
            var command = new UpdateBidStatusCommand{Id = id, StatusId = model.StatusId};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}