using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TenderManagementSystem.Core.Exceptions;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Domain.Models.Entities.RBAC;
using TenderManagementSystem.Infrastructure.Abstractions;

namespace TenderManagementSystem.Application.Commands.Vendors

{
    public class CreateVendorCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string UserId { get; set; }
    }

    public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, string>
    {
        private readonly IBaseRepository<Vendor> _repository;
        private readonly IVendorQueryRepository _dapper;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;


        public CreateVendorCommandHandler(IBaseRepository<Vendor> repository, UserManager<User> userManager,
            IMapper mapper,IVendorQueryRepository dapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
        }

        public async Task<string> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException($"User with ID {request.UserId} not found");
            var existingVendor = await _dapper.GetByUserIdAsync(request.UserId);
            if (existingVendor != null)
                throw new BusinessException("This user already has a vendor profile");

            var vendor = _mapper.Map<Vendor>(request);
            var entity = await _repository.CreateAsync(vendor);
            return entity.Id;
        }
    }
}