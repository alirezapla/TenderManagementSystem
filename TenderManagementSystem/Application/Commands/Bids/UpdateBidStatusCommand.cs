using AutoMapper;
using MediatR;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Infrastructure.Abstractions;

namespace TenderManagementSystem.Application.Commands.Bids
{
    public class UpdateBidStatusCommand : IRequest<BidDto>
    {
        public string Id { get; set; }
        public string StatusId { get; set; }
    }

    public class UpdateBidStatusCommandHandler : IRequestHandler<UpdateBidStatusCommand, BidDto>
    {
        private readonly IBaseRepository<Bid> _repository;
        private readonly IMapper _mapper;

        public UpdateBidStatusCommandHandler(IBaseRepository<Bid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BidDto> Handle(UpdateBidStatusCommand request, CancellationToken cancellationToken)
        {
            var updatedBid = await _repository.UpdatePartialAsync(request.Id, bid =>
            {
                bid.StatusId = request.StatusId;
                bid.UpdatedDate = DateTime.UtcNow;
            });
            return _mapper.Map<BidDto>(updatedBid);
        }
    }
}