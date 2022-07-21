using Versioning.Domain.ClusterConfigurations;
using Moq;
using Versioning.Shared.Tests;

namespace Versioning.Service.Tests.ClusterConfigurations
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
