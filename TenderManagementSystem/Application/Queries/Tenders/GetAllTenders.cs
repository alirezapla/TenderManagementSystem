using AutoMapper;
using Dapper;
using MediatR;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Core.Exceptions;
using TenderManagementSystem.Core.Models;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Infrastructure.Data.Dapper;

namespace TenderManagementSystem.Application.Queries.Tenders

{
    public class GetTendersQuery : IRequest<PagedResult<TenderDto>>
    {
        public QueryParams QueryParams { get; set; }
    }

    public class GetTendersQueryHandler : IRequestHandler<GetTendersQuery, PagedResult<TenderDto>>
    {
        private readonly IDapperContext _dapperContext;
        private readonly IMapper _mapper;

        public GetTendersQueryHandler(IDapperContext dapperContext, IMapper mapper)
        {
            _dapperContext = dapperContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<TenderDto>> Handle(GetTendersQuery request,
            CancellationToken cancellationToken)
        {
            using var connection = _dapperContext.CreateConnection();

            var countQuery = "SELECT COUNT(*) FROM Tenders";
            var totalCount = await connection.ExecuteScalarAsync<int>(countQuery);

            var query = @"
                SELECT 
            t.Id, t.Title, t.Description, t.Deadline,
            c.Id, c.Name,  
            s.Id, s.Name   
        FROM Tenders t
        JOIN Categories c ON t.CategoryId = c.Id
        JOIN Statuses s ON t.StatusId = s.Id
        WHERE t.IsDeleted = 0
        ORDER BY t.CreatedDate DESC
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var skip = (request.QueryParams.PaginationParams.Number - 1) * request.QueryParams.PaginationParams.Size;
            var take = request.QueryParams.PaginationParams.Size;

            var tenders = await connection.QueryAsync<TenderDto, CategoryDto, StatusDto, TenderDto>(
                query,
                (tender, category, status) =>
                {
                    tender.Category = category;
                    tender.Status = status;
                    return tender;
                },
                new { Skip = skip, Take = take },
                splitOn: "Id,Id"  
            );

            return new PagedResult<TenderDto>(tenders.ToList(), totalCount, request.QueryParams.PaginationParams);
        }
    }
}