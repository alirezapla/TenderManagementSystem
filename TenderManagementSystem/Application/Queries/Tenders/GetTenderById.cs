using AutoMapper;
using Dapper;
using MediatR;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Core.Exceptions;
using TenderManagementSystem.Core.Models.DTOs;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Infrastructure.Data.Dapper;


namespace TenderManagementSystem.Application.Queries.Tenders

{
    public class GetTenderByIdQuery : IRequest<TenderDetailsDto>
    {
        public string TenderId { get; set; }
    }

    public class GetTenderByIdQueryHandler : IRequestHandler<GetTenderByIdQuery, TenderDetailsDto>
    {
        private readonly IDapperContext _dapperContext;
        private readonly IMapper _mapper;

        public GetTenderByIdQueryHandler(IDapperContext dapperContext, IMapper mapper)
        {
            _dapperContext = dapperContext;
            _mapper = mapper;
        }

        public async Task<TenderDetailsDto> Handle(GetTenderByIdQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"
            SELECT 
                t.Id, t.Title, t.Description, t.Deadline,
                c.Id AS CategoryId, c.Name AS CategoryName,
                s.Id AS StatusId, s.Name AS StatusName
            FROM Tenders t
            JOIN Categories c ON t.CategoryId = c.Id
            JOIN Statuses s ON t.StatusId = s.Id
            WHERE t.Id = @Id;

            SELECT 
                b.Id, b.Amount, b.SubmissionDate,
                v.Id AS VendorId, v.Name AS VendorName,
                bs.Id AS StatusId, bs.Name AS StatusName
            FROM Bids b
            JOIN Vendors v ON b.VendorId = v.Id
            JOIN Statuses bs ON b.StatusId = bs.Id
            WHERE b.TenderId = @Id;
        ";

            using var conn = _dapperContext.CreateConnection();
            using var multi = await conn.QueryMultipleAsync(sql, new {Id = request.TenderId});

            var tenderRow = await multi.ReadSingleOrDefaultAsync<dynamic>();
            if (tenderRow == null) throw new NotFoundException($"Tender {request.TenderId} not found.");

            var tender = new TenderDetailsDto
            {
                Id = tenderRow.Id,
                Title = tenderRow.Title,
                Description = tenderRow.Description,
                Deadline = tenderRow.Deadline,
                Category = new CategoryDto
                {
                    Id = tenderRow.CategoryId,
                    Name = tenderRow.CategoryName
                },
                Status = new StatusDto
                {
                    Id = tenderRow.StatusId,
                    Name = tenderRow.StatusName
                }
            };

            var bidRows = await multi.ReadAsync<dynamic>();

            foreach (var bid in bidRows)
            {
                tender.Bids.Add(new BidDto
                {
                    Id = bid.Id,
                    Amount = bid.Amount,
                    SubmissionDate = bid.SubmissionDate,
                    Vendor = new VendorDto
                    {
                        Id = bid.VendorId,
                        Name = bid.VendorName
                    },
                    Status = new StatusDto
                    {
                        Id = bid.StatusId,
                        Name = bid.StatusName
                    }
                });
            }

            return tender;
        }
    }
}