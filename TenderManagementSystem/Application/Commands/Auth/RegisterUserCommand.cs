using MediatR;
using Microsoft.AspNetCore.Identity;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Core.Exceptions;
using TenderManagementSystem.Domain.Models.Entities.RBAC;

namespace TenderManagementSystem.Application.Commands.Auth
{
    public class RegisterUserCommand : IRequest<RegistrationResult>
    {
        public RegisterUserDTO Model { get; set; }
    }

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegistrationResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly PasswordHasher<User> _hasher;


        public RegisterUserHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _hasher = new PasswordHasher<User>();
        }

        public async Task<RegistrationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByNameAsync(request.Model.Username);
            if (userExists != null)
                throw new UserRegistrationException("User already exists");

            return await CreateUser(request);
        }

        private async Task<RegistrationResult> CreateUser(RegisterUserCommand request)
        {
            if (!await _roleManager.RoleExistsAsync(request.Model.Role))
                throw new UserRegistrationException("Role does not exist");

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Model.Username
            };

            var createUserResult = await _userManager.CreateAsync(user, request.Model.Password);
            if (!createUserResult.Succeeded)
                throw new UserRegistrationException("User creation failed! Please check user details and try again.");

            var roleResult = await _userManager.AddToRoleAsync(user, request.Model.Role);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new UserRegistrationException("Failed to register user");
            }

            return new RegistrationResult(user.Id);
        }
    }

    public class RegistrationResult
    {
        public bool Succeeded { get; }
        public string UserId { get; }
        public IEnumerable<IdentityError> Errors { get; }

        public RegistrationResult(string userId)
        {
            Succeeded = true;
            UserId = userId;
        }

        public RegistrationResult(IdentityResult result)
        {
            Succeeded = result.Succeeded;
            Errors = result.Errors;
        }
    }
}