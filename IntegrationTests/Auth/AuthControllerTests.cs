using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Domain.Models.Entities.RBAC;
using TenderManagementSystem.Infrastructure.Data;
using Xunit;

namespace UnitTests.Controller;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public AuthControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_WithValidAdminRole_ReturnsOkAndCreatesUser()
        {
            // Arrange
            var request = new RegisterUserDTO
            {
                Username = "testuser1",
                Password = "Testpass123",
                Role = "Admin"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var user = await userManager.FindByNameAsync(request.Username);
            Assert.NotNull(user);
            Assert.Equal(request.Username, user.UserName);
            Assert.False(user.IsDeleted);
            Assert.True(await userManager.IsInRoleAsync(user, "Admin"));

            var userRoles = await dbContext.UserRoles
                .Where(ur => ur.UserId == user.Id && ur.RoleId == "0021877C-3F5F-45E3-BE2C-A339601532B6")
                .ToListAsync();
            Assert.Single(userRoles);
        }

        [Fact]
        public async Task Register_WithValidVendorRole_ReturnsOkAndCreatesUser()
        {
            // Arrange
            var request = new RegisterUserDTO
            {
                Username = "testuser2",
                Password = "Testpass123",
                Role = "Vendor"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var user = await userManager.FindByNameAsync(request.Username);
            Assert.NotNull(user);
            Assert.Equal(request.Username, user.UserName);
            Assert.False(user.IsDeleted);
            Assert.True(await userManager.IsInRoleAsync(user, "Vendor"));

            var userRoles = await dbContext.UserRoles
                .Where(ur => ur.UserId == user.Id && ur.RoleId == "4AA5C7A2-9769-4D6D-B0FB-4C1EE0CD7B22")
                .ToListAsync();
            Assert.Single(userRoles);
        }

        [Fact]
        public async Task Register_WithNonExistentRole_ReturnsBadRequest()
        {
            // Arrange
            var request = new RegisterUserDTO
            {
                Username = "testuser3",
                Password = "Testpass123",
                Role = "InvalidRole"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Role 'InvalidRole' does not exist", content);

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = await userManager.FindByNameAsync(request.Username);
            Assert.Null(user);
        }

        [Fact]
        public async Task Register_WithDuplicateUsername_ReturnsBadRequest()
        {
            // Arrange
            var request1 = new RegisterUserDTO
            {
                Username = "testuser4",
                Password = "Testpass123",
                Role = "Admin"
            };
            await _client.PostAsJsonAsync("/api/auth/register", request1);

            var request2 = new RegisterUserDTO
            {
                Username = "testuser4",
                Password = "Testpass123",
                Role = "Vendor"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", request2);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Username 'testuser4' is already taken", content);
        }
    }