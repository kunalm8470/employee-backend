using AutoMapper;
using Xunit;

namespace Employees.UnitTests.AutoMapper
{
    public class MappingProfileTests : IClassFixture<MappingFixture>
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;
        public MappingProfileTests(MappingFixture mappingFixture)
        {
            _configurationProvider = mappingFixture.ConfigurationProvider;
            _mapper = mappingFixture.Mapper;
        }

        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            _configurationProvider.AssertConfigurationIsValid();
        }
    }
}
