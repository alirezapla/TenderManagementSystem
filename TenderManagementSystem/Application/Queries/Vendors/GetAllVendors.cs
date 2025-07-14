using AutoMapper;
using Dapper;
using MediatR;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Core.Models;
using TenderManagementSystem.Core.Models.DTOs;
using TenderManagementSystem.Infrastructure.Data.Dapper;

namespace TenderManagementSystem.Application.Queries.Vendors

{
    public class GetVendorsQuery : IRequest<PagedResult<VendorDto>>
    {
        public QueryParams QueryParams { get; set; }

    }

    public class GetVendorsQueryHandler : IRequestHandler<GetVendorsQuery, PagedResult<VendorDto>>
    {
        private readonly IDapperContext _dapperContext;
        private readonly IMapper _mapper;

        public GetVendorsQueryHandler(IDapperContext dapperContext, IMapper mapper)
        {
            _dapperContext = dapperContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<VendorDto>> Handle(GetVendorsQuery request, CancellationToken cancellationToken)
        {
            await using var connection = _dapperContext.CreateConnection();
            
            var countQuery = "SELECT COUNT(*) FROM Vendors";
            var totalCount = await connection.ExecuteScalarAsync<int>(countQuery);
            var skip = (request.QueryParams.PaginationParams.Number - 1) * request.QueryParams.PaginationParams.Size;
            var take = request.QueryParams.PaginationParams.Size;
            var query = @"
                SELECT v.Id, v.Name,
                       (SELECT COUNT(*) FROM Bids b WHERE b.VendorId = v.Id) as BidCount
                FROM Vendors v
                ORDER BY v.CreatedDate DESC
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var vendors = await connection.QueryAsync<VendorDto>(
                query,
                new { Skip = skip, Take = take });

            return new PagedResult<VendorDto>(vendors.ToList(), totalCount,request.QueryParams.PaginationParams);
        }
    }
}