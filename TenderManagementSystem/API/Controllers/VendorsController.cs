using MediatR;
using Microsoft.AspNetCore.Mvc;
using TenderManagementSystem.Application.Commands.Vendors;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Application.Queries.Vendors;
using TenderManagementSystem.Core.Models;

namespace TenderManagementSystem.API.Controllers
{
    [Route("[controller]")]
    public class VendorsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public VendorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetVendorsQuery
            {
                QueryParams = new QueryParams
                {
                    PaginationParams = new PaginationParams { Number = pageNumber, Size = pageSize }
                }

            };
            var vendors = await _mediator.Send(query);
            return Ok(vendors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var vendor = await _mediator.Send(new GetVendorByIdQuery {Id = id});
            if (vendor == null) return NotFound();
            return Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVendor([FromBody] CreateVendorDto model)
        {
            var command = new CreateVendorCommand
            {
                Name = model.Name,
                UserId = model.UserId
            };
            var vendorId = await _mediator.Send(command);
            return Ok(new {id = vendorId});
        }
    }
}