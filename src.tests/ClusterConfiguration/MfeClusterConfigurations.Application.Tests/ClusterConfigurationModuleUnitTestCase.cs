using MfeClusterConfigurations.Domain;
using Moq;
using Versioning.Shared.Tests;

namespace MfeClusterConfigurations.Application.Tests
{
    public abstract class ClusterConfigurationModuleUnitTestCase : UnitTestCase
    {
        protected Mock<IClusterConfigurationRepository> Repository { get; private set; }

        protected ClusterConfigurationModuleUnitTestCase()
        {
            this.Repository = new Mock<IClusterConfigurationRepository>();
        }

        protected void ShouldHaveSave(ClusterConfiguration configuration)
        {
            this.Repository.Verify(x => x.Save(configuration), Times.AtLeastOnce());
        }
    }
}
