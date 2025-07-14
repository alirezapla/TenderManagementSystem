using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenderManagementSystem.Core.Models;
using TenderManagementSystem.Infrastructure.Filters;

namespace TenderManagementSystem.API.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogActionFilter))]
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();

        protected QueryParams BuildQueryParams(
            int pageNumber = 1,
            int pageSize = 10,
            string sortBy = "Id",
            bool sortDescending = false)
        {
            return new QueryParams
            {
                PaginationParams = new PaginationParams {Number = pageNumber, Size = pageSize},
                SortParams = new SortParams {SortBy = sortBy, SortDescending = sortDescending},
                
            };
        }
    }
}