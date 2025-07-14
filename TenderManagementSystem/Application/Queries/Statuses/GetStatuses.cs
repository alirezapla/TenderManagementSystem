using Dapper;
using MediatR;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Infrastructure.Data.Dapper;

namespace TenderManagementSystem.Application.Queries.Status

{
    public class GetStatuses : IRequest<List<StatusDto>>
    {
    }

    public class GetStatusesQueryHandler : IRequestHandler<GetStatuses, List<StatusDto>>
    {
        private readonly IDapperContext _dapperContext;

        public GetStatusesQueryHandler(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<List<StatusDto>> Handle(GetStatuses request, CancellationToken cancellationToken)
        {
            await using var connection = _dapperContext.CreateConnection();
            const string query = "SELECT Id, Name FROM Statuses";
            var statuses = await connection.QueryAsync<StatusDto>(query);
            return statuses.ToList();
        }
    }
}