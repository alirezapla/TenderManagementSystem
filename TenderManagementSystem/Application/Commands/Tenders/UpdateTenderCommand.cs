using AutoMapper;
using MediatR;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Infrastructure.Abstractions;

namespace TenderManagementSystem.Application.Commands.Tenders;

public class UpdateTenderCommand : IRequest<TenderDto>
{
    public UpdateTenderDTO Tender { get; set; }
    public string Id { get; set; }
}

public class UpdateTenderCommandHandler : IRequestHandler<UpdateTenderCommand, TenderDto>
{
    private readonly IBaseRepository<Tender> _repository;
    private readonly IMapper _mapper;

    public UpdateTenderCommandHandler(IBaseRepository<Tender> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TenderDto> Handle(UpdateTenderCommand request, CancellationToken cancellationToken)
    {
        var tender = _mapper.Map<Tender>(request.Tender);
        tender.Id = request.Id; 
        
        var updatedTender = await _repository.UpdateAsync(tender);
        return _mapper.Map<TenderDto>(updatedTender);
    }
}