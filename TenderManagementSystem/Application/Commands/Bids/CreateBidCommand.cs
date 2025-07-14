using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Core.Exceptions;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Infrastructure.Abstractions;
using TenderManagementSystem.Infrastructure.Data.Dapper;

namespace TenderManagementSystem.Application.Commands.Bids
{
    public class CreateBidCommand : IRequest<string>
    {
        public CreateBidDto Bid { get; set; }
    }

    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, string>
    {
        private readonly IBaseRepository<Bid> _repository;
        private readonly IDapperContext _dapperContext;
        private readonly IMapper _mapper;

        public CreateBidCommandHandler(IBaseRepository<Bid> repository, IMapper mapper, IDapperContext dapperContext)
        {
            _repository = repository;
            _mapper = mapper;
            _dapperContext = dapperContext;

        }

        public async Task<string> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        {
            var bid = _mapper.Map<Bid>(request.Bid);
            await using var connection = _dapperContext.CreateConnection();
            await Validate(connection, request.Bid);
            bid.StatusId = await GetStatusId(connection,"pending");
            var entity= await _repository.CreateAsync(bid);
            return entity.Id;
        }

        private async Task Validate(SqlConnection connection,CreateBidDto bid)
        {
            var tenderId = bid.TenderId;
            const string queryT = "SELECT 1 FROM Tenders WHERE Id = @TenderId AND IsDeleted = 0";
            var tenderExists = await connection.QueryFirstOrDefaultAsync<int?>(queryT, new { TenderId = tenderId });

            if (tenderExists == null)
            {
                throw new NotFoundException($"Tender with ID {tenderId} not found");
            }

            var vendorId = bid.VendorId;
            const string queryV = "SELECT 1 FROM Vendors WHERE Id = @vendorId AND IsDeleted = 0";
            var vendorExists = await connection.QueryFirstOrDefaultAsync<int?>(queryV, new { VendorId = vendorId });

            if (tenderExists == null)
            {
                throw new NotFoundException($"Vendor with ID {vendorId} not found");
            }
        }
        private async Task<string> GetStatusId(SqlConnection connection,string statusName)
        {
            
            const string query = "SELECT Id FROM Statuses where Name = @statusName";
            var pendingStatus = await connection.QueryAsync<string>(query,new { statusName = statusName });
            if (pendingStatus == null)
            {
                throw new Exception("Pending status not found");
            }

            return pendingStatus?.FirstOrDefault();
        }
    }
}