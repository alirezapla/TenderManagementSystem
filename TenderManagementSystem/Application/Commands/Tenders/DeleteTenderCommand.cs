using MediatR;
using TenderManagementSystem.Core.Exceptions;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Infrastructure.Abstractions;

namespace TenderManagementSystem.Application.Commands.Tenders;

public class DeleteTenderCommand : IRequest<string>
{
    public string Id { get; set; }


    public class DeleteTenderCommandHandler : IRequestHandler<DeleteTenderCommand, string>
    {
        private readonly IBaseRepository<Tender> _repository;

        public DeleteTenderCommandHandler(IBaseRepository<Tender> repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(DeleteTenderCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.SoftDeleteAsync(request.Id);
            if (result == 0)
            {
                throw new NotFoundException(request.Id);
            }
            
            return $"Tender with Id {request.Id} has been deleted";
        }
    }
}