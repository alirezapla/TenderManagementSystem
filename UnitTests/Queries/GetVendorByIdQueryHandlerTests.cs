using FluentAssertions;
using NSubstitute;
using TenderManagementSystem.Application.Queries.Vendors;
using TenderManagementSystem.Core.Exceptions;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Infrastructure.Abstractions;
using Xunit;

namespace UnitTests.Queries;

public class GetVendorByIdQueryHandlerTests : TestBase
{
    private readonly GetVendorByIdQueryHandler _sut;
    private readonly IVendorQueryRepository _repository = Substitute.For<IVendorQueryRepository>();

    public GetVendorByIdQueryHandlerTests()
    {
        _sut = new GetVendorByIdQueryHandler(_repository);
    }

    [Fact]
    public async Task Handle_ShouldReturnVendor_WhenExists()
    {
        // Arrange
        var vendorId = Guid.NewGuid().ToString();
        var expectedVendor = Fixture.Build<Vendor>()
            .With(x => x.Id, vendorId)
            .Create();
            
        _repository.GetByIdAsync(vendorId).Returns(expectedVendor);

        // Act
        var result = await _sut.Handle(new GetVendorByIdQuery { Id = vendorId }, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedVendor);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenVendorNotFound()
    {
        // Arrange
        var vendorId = Guid.NewGuid().ToString();
        _repository.GetByIdAsync(vendorId).Returns((Vendor)null);

        // Act & Assert
        await FluentActions.Invoking(() => 
                _sut.Handle(new GetVendorByIdQuery { Id = vendorId }, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Vendor with ID {vendorId} not found");
    }
}