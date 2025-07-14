using Dapper;
using MediatR;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Infrastructure.Data.Dapper;

namespace TenderManagementSystem.Application.Queries.Categories

{
    public class GetCategories : IRequest<List<CategoryDto>>
    {
    }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategories, List<CategoryDto>>
    {
        private readonly IDapperContext _dapperContext;

        public GetCategoriesQueryHandler(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<List<CategoryDto>> Handle(GetCategories request, CancellationToken cancellationToken)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = "SELECT Id, Name FROM Categories";
            var categories = await connection.QueryAsync<CategoryDto>(query);
            return categories.ToList();
        }
    }
}