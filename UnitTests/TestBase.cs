using AutoMapper;

namespace UnitTests

{
    public abstract class TestBase
    {
        protected readonly IFixture Fixture;
        protected readonly IMapper Mapper;

        protected TestBase()
        {
            Fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            
            var config = new MapperConfiguration(cfg => 
                cfg.AddMaps(typeof(YourMappingProfile).Assembly);
            
            Mapper = config.CreateMapper();
            Fixture.Inject(Mapper);
        }
    }