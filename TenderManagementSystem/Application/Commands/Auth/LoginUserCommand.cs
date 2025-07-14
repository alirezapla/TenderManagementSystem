using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Core.Exceptions;
using TenderManagementSystem.Domain.Models.Entities.RBAC;
using TenderManagementSystem.Domain.Services.Abstractions;
using TenderManagementSystem.Infrastructure.Data;

namespace TenderManagementSystem.Application.Commands.Auth
{
    public class LoginUserCommand : IRequest<UserPrincipalDTO>
    {
        public LoginDTO Model { get; set; }


        public class LoginHandler : IRequestHandler<LoginUserCommand, UserPrincipalDTO>
        {
            private readonly ILoggerManager _logger;
            private readonly IConfiguration _configuration;
            private readonly AppDbContext _context;
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<Role> _roleManager;

            public LoginHandler(ILoggerManager logger, IConfiguration configuration, AppDbContext context,
                UserManager<User> userManager, RoleManager<Role> roleManager)
            {
                _logger = logger;
                _configuration = configuration;
                _context = context;
                _userManager = userManager;
                _roleManager = roleManager;
            }


            public async Task<UserPrincipalDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Model.Username);
                if (user == null || !(await _userManager.CheckPasswordAsync(user, request.Model.Password)))
                {
                    _logger.LogWarn($"{nameof(LoginUserCommand)}: Login failed. Wrong Username or password");
                    throw new LogInFailedException("Login failed. Wrong Username or password");
                }

                var roles = await _userManager.GetRolesAsync(user);
                var token = await CreateToken(user, roles);
                return new UserPrincipalDTO
                {
                    Message = "User logged in successfully",
                    Username = user.UserName,
                    Token = token
                };
            }


            private async Task<string> CreateToken(User user, IList<string> roles)
            {
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims(user, roles);
                var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

                return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            }

            private SigningCredentials GetSigningCredentials()
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var key = Encoding.UTF8.GetBytes(jwtSettings["key"]);
                var secret = new SymmetricSecurityKey(key);

                return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            }

            private async Task<IReadOnlyList<Claim>> GetClaims(User user, IList<string> roleNames)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.UserName),
                    new(ClaimTypes.Sid, user.Id),
                    new(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString("yyyy-MM-dd"))
                };

                foreach (var roleName in roleNames)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));

                    var role = await _roleManager.Roles
                        .Include(r => r.Permissions)
                        .FirstOrDefaultAsync(r => r.Name == roleName);

                    if (role?.Permissions != null)
                    {
                        foreach (var permission in role.Permissions)
                        {
                            claims.Add(new Claim("permission", permission.Name));
                        }
                    }
                }

                return claims;
            }

            private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
                IReadOnlyList<Claim> claims)
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var tokenOptions = new JwtSecurityToken
                (
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                    signingCredentials: signingCredentials
                );

                return tokenOptions;
            }
        }
    }
}