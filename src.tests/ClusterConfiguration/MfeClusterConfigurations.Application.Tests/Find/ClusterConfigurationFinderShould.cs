using System.Threading.Tasks;
using MfeClusterConfigurations.Application.Find;
using MfeClusterConfigurations.Domain.Exceptions;
using MfeClusterConfigurations.Domain.Tests;
using MfeGlobalConfigurations.Domain;
using MfeGlobalConfigurations.Domain.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Versioning.Shared.Domain.Exceptions;
using Versioning.Shared.Tests.Domain;

namespace MfeClusterConfigurations.Application.Tests.Find
{
    [TestClass]
    public class ClusterConfigurationFinderShould : ClusterConfigurationModuleUnitTestCase
    {
        private readonly ClusterConfigurationFinder finder;
        private readonly GlobalConfigurationFinder globalConfigurationFinder;
        private readonly Mock<IGlobalConfigurationRepository> globalConfigurationRepository;
        public ClusterConfigurationFinderShould()
        {
            this.globalConfigurationRepository = new Mock<IGlobalConfigurationRepository>();
            this.globalConfigurationFinder = new GlobalConfigurationFinder(this.globalConfigurationRepository.Object);
            this.finder = new ClusterConfigurationFinder(this.Repository.Object, this.globalConfigurationFinder);
        }

        [TestMethod]
        public async Task It_Should_Find_ClusterConfiguration()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = ConfigurationNameMother.Random();
            var clusterConfigurationResult = ClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.First(3), activeConfiguration);
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, activeConfiguration);

            Assert.AreEqual<string>(response.VersionUrl, clusterConfigurationResult.Configurations[activeConfiguration].Value);
            Assert.AreEqual(response.ConfigurationSource, $"cluster - {activeConfiguration.Value}");
        }

        [TestMethod]
        [ExpectedException(typeof(NoActiveClusterConfigurationExistsException))]
        public async Task It_Should_Has_Error_with_Active_Empty_Configuration()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = ConfigurationNameMother.Create("");
            var clusterConfigurationResult = ClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.Random(), activeConfiguration);
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationNotSupportedException))]
        public async Task It_Should_Has_Error_with_Empty_Configuration()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = ConfigurationNameMother.Create("");
            var clusterConfigurationResult = ClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.Random(), activeConfiguration);
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, activeConfiguration);
        }

        [TestMethod]
        [ExpectedException(typeof(ClusterInvalidActiveConfigurationException))]
        public async Task It_Should_Has_Error_with_No_Active_Configuration_Match()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = ConfigurationNameMother.Create("current");
            var clusterConfigurationResult = ClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.For(new string[] { "previous" }), activeConfiguration);
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ClusterInvalidConfigurationException))]
        public async Task It_Should_Has_Error_with_No_Configuration_Match()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = ConfigurationNameMother.Create("current");
            var clusterConfigurationResult = ClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.For(new string[] { "previous" }), activeConfiguration);
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, activeConfiguration);
        }

        [TestMethod]
        public async Task It_Should_Find_Global_Configuration()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = ConfigurationNameMother.Random();
            var clusterConfigurationResult = ClusterConfigurationMother.Empty();//No Cluster configuration
            var globalConfigurationResult = GlobalConfigurationMother.Random();
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);
            this.globalConfigurationRepository.Setup(g => g.Search(mfeId)).ReturnsAsync(globalConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, activeConfiguration);

            Assert.AreEqual<string>(response.VersionUrl, globalConfigurationResult.Configurations[activeConfiguration].Value);
            ;
            Assert.AreEqual(response.ConfigurationSource, $"global - {activeConfiguration.Value}");
        }

        [TestMethod]
        [ExpectedException(typeof(ClusterConfigurationDoesntExistsException))]
        public async Task It_Should_Has_Error_No_Configuration_at_All()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = ConfigurationNameMother.Random();
            var clusterConfigurationResult = ClusterConfigurationMother.Empty();//No Cluster configuration
            var globalConfigurationResult = GlobalConfigurationMother.Empty();
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);
            this.globalConfigurationRepository.Setup(g => g.Search(mfeId)).ReturnsAsync(globalConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, activeConfiguration);

        }
    }
}
