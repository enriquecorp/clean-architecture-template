using MfeClusterConfigurations.Domain;
using Moq;
using Versioning.Shared.Tests;

namespace MfeClusterConfigurations.Application.Tests
{
    public abstract class ClusterConfigurationModuleUnitTestCase : UnitTestCase
    {
        protected Mock<IMfeClusterConfigurationRepository> Repository { get; private set; }

        protected ClusterConfigurationModuleUnitTestCase()
        {
            this.Repository = new Mock<IMfeClusterConfigurationRepository>();
        }

        protected void ShouldHaveSave(MfeClusterConfiguration configuration)
        {
            this.Repository.Verify(x => x.Save(configuration), Times.AtLeastOnce());
        }
    }
}
