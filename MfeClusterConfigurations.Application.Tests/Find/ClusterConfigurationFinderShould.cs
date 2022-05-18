using MfeClusterConfigurations.Application.Find;
using MfeClusterConfigurations.Domain.Tests;
using MfeGlobalConfigurations.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Versioning.Shared.Tests.Domain;

namespace MfeClusterConfigurations.Application.Tests.Find
{
    [TestClass]
    public class ClusterConfigurationFinderShould : ClusterConfigurationModuleUnitTestCase
    {
        private readonly MfeClusterConfigurationFinder finder;
        private readonly MfeGlobalConfigurationFinder globalConfigurationFinder;
        private readonly Mock<IMfeGlobalConfigurationRepository> globalConfigurationRepository;
        public ClusterConfigurationFinderShould()
        {
            this.globalConfigurationRepository = new Mock<IMfeGlobalConfigurationRepository>();
            this.globalConfigurationFinder = new MfeGlobalConfigurationFinder(this.globalConfigurationRepository.Object);
            this.finder = new MfeClusterConfigurationFinder(this.Repository.Object, this.globalConfigurationFinder);
        }

        [TestMethod]
        public void It_Should_Find_ClusterConfiguration()
        {
            this.Repository.Setup(r => r.Search(MfeIdMother.Random(), ClusterIdMother.Random())).ReturnsAsync(MfeClusterConfigurationMother.Random());
        }
    }
}
