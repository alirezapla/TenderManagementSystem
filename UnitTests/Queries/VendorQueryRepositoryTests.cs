namespace UnitTests.Queries;


    public class VendorQueryRepositoryTests
    {
        private readonly VendorQueryRepository _sut;
        private readonly IDapperContext _context = Substitute.For<IDapperContext>();
        private readonly IDbConnection _connection = Substitute.For<IDbConnection>();

        public VendorQueryRepositoryTests()
        {
            _context.CreateConnection().Returns(_connection);
            _sut = new VendorQueryRepository(_context);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnVendor_WhenExists()
        {
            // Arrange
            var vendorId = Guid.NewGuid().ToString();
            var expectedVendor = new Vendor { Id = vendorId, Name = "Test Vendor" };
            
            _connection.QuerySingleOrDefaultAsync<Vendor>(Arg.Any<string>(), Arg.Any<object>())
                .Returns(expectedVendor);

            // Act
            var result = await _sut.GetByIdAsync(vendorId);

            // Assert
            result.Should().BeEquivalentTo(expectedVendor);
            await _connection.Received(1).QuerySingleOrDefaultAsync<Vendor>(
                Arg.Is<string>(s => s.Contains("SELECT * FROM Vendors")),
                Arg.Is<object>(o => (string)o.GetType().GetProperty("Id").GetValue(o) == vendorId));
        }

        [Fact]
        public async Task GetWithDetailsAsync_ShouldJoinTablesCorrectly()
        {
            // Arrange
            var vendorId = Guid.NewGuid().ToString();
            var expectedVendor = new Vendor { Id = vendorId, Name = "Test Vendor" };
            
            _connection.QuerySingleOrDefaultAsync<Vendor>(Arg.Any<string>(), Arg.Any<object>())
                .Returns(expectedVendor);

            // Act
            var result = await _sut.GetWithDetailsAsync(vendorId);

            // Assert
            await _connection.Received(1).QuerySingleOrDefaultAsync<Vendor>(
                Arg.Is<string>(s => s.Contains("JOIN Users u ON v.UserId = u.Id")),
                Arg.Any<object>());
        }
    }
}