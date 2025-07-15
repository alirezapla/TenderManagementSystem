namespace UnitTests.Commands;

public class CreateVendorCommandHandlerTests 
    {
        private readonly CreateVendorCommandHandler _sut;
        private readonly IBaseRepository<Vendor> _repository = Substitute.For<IBaseRepository<Vendor>>();
        private readonly UserManager<User> _userManager = Substitute.For<UserManager<User>>(
            Substitute.For<IUserStore<User>>(), null, null, null, null, null, null, null, null);

        public CreateVendorCommandHandlerTests()
        {
            _sut = new CreateVendorCommandHandler(_repository, _userManager, Mapper);
        }

        [Fact]
        public async Task Handle_ShouldCreateVendor_WhenValidRequest()
        {
            // Arrange
            var command = Fixture.Build<CreateVendorCommand>()
                .With(x => x.Name, "Test Vendor")
                .With(x => x.UserId, Guid.NewGuid().ToString())
                .Create();
            
            _userManager.FindByIdAsync(command.UserId).Returns(new User());
            _repository.FindAsync(Arg.Any<Expression<Func<Vendor, bool>>>()).Returns((Vendor)null);
            _repository.CreateAsync(Arg.Any<Vendor>()).Returns(Task.CompletedTask);

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _repository.Received(1).CreateAsync(Arg.Is<Vendor>(v => 
                v.Name == command.Name && 
                v.UserId == command.UserId));
            
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenUserNotFound()
        {
            // Arrange
            var command = Fixture.Create<CreateVendorCommand>();
            _userManager.FindByIdAsync(command.UserId).Returns((User)null);

            // Act & Assert
            await FluentActions.Invoking(() => _sut.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<NotFoundException>()
                .WithMessage($"User with ID {command.UserId} not found");
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenVendorExistsForUser()
        {
            // Arrange
            var command = Fixture.Create<CreateVendorCommand>();
            _userManager.FindByIdAsync(command.UserId).Returns(new User());
            _repository.FindAsync(Arg.Any<Expression<Func<Vendor, bool>>>())
                .Returns(new Vendor());

            // Act & Assert
            await FluentActions.Invoking(() => _sut.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<BusinessException>()
                .WithMessage("This user already has a vendor profile");
        }
    }