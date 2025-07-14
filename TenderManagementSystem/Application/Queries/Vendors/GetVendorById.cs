using Dapper;
using MediatR;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Core.Models.DTOs;
using TenderManagementSystem.Infrastructure.Data.Dapper;


namespace TenderManagementSystem.Application.Queries.Vendors
{
    public class GetVendorByIdQuery : IRequest<VendorDto>
    {
        public string Id { get; set; }
    }

    public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, VendorDto>
    {
        private readonly IDapperContext _dapperContext;

        public GetVendorByIdQueryHandler(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<VendorDto> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dapperContext.CreateConnection();

            // Query for vendor basic info
            var vendorQuery = "SELECT Id, Name FROM Vendors WHERE Id = @Id";
            var vendor = await connection.QueryFirstOrDefaultAsync<VendorDto>(vendorQuery, new {request.Id});

            if (vendor == null)
            {
                return null;
            }

            // Query for bids with related data
            var bidsQuery = @"
        SELECT 
            b.Id, b.Amount, b.SubmissionDate,
            t.Title AS TenderTitle,
            s.Id AS StatusId, s.Name AS StatusName
        FROM Bids b
        JOIN Tenders t ON b.TenderId = t.Id
        JOIN Statuses s ON b.StatusId = s.Id
        WHERE b.VendorId = @VendorId";

            var bidResults = await connection.QueryAsync<BidResult>(bidsQuery, new {VendorId = request.Id});

            // Map results to DTOs
            vendor.Bids = bidResults.Select(b => new VendorBidsDto
            {
                Id = b.Id,
                Amount = b.Amount,
                SubmissionDate = b.SubmissionDate,
                TenderTitle = b.TenderTitle,
                Status = new StatusDto {Id = b.StatusId, Name = b.StatusName},
            }).ToList();

            vendor.BidCount = vendor.Bids.Count;

            return vendor;
        }
    }
    public class BidResult
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string TenderTitle { get; set; }
        public string StatusId { get; set; }
        public string StatusName { get; set; }
    }
}