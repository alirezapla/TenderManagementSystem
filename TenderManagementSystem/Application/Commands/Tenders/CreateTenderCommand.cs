using AutoMapper;
using MediatR;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Infrastructure.Abstractions;

namespace TenderManagementSystem.Application.Commands.Tenders

{
    public class CreateTenderCommand : IRequest<string>
    {
        public CreateTenderDto Tender { get; set; }
    }

    public class CreateTenderCommandHandler : IRequestHandler<CreateTenderCommand, string>
    {
        private readonly IBaseRepository<Tender> _repository;
        private readonly IMapper _mapper;


        public CreateTenderCommandHandler(IBaseRepository<Tender> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateTenderCommand request, CancellationToken cancellationToken)
        {
            var tender = _mapper.Map<Tender>(request.Tender);
            var entity = await _repository.CreateAsync(tender);
            return entity.Id;
        }
    }
}