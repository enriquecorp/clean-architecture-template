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
        public async Task It_Should_Find_ClusterConfiguration()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = MfeConfigurationNameMother.Random();
            var clusterConfigurationResult = MfeClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.First(3), activeConfiguration);
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
            var activeConfiguration = MfeConfigurationNameMother.Create("");
            var clusterConfigurationResult = MfeClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.Random(), activeConfiguration);
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationNotSupportedException))]
        public async Task It_Should_Has_Error_with_Empty_Configuration()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = MfeConfigurationNameMother.Create("");
            var clusterConfigurationResult = MfeClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.Random(), activeConfiguration);
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, activeConfiguration);
        }

        [TestMethod]
        [ExpectedException(typeof(MfeClusterInvalidActiveConfigurationException))]
        public async Task It_Should_Has_Error_with_No_Active_Configuration_Match()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = MfeConfigurationNameMother.Create("current");
            var clusterConfigurationResult = MfeClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.For(new string[] { "previous" }), activeConfiguration);
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, null);
        }

        [TestMethod]
        [ExpectedException(typeof(MfeClusterInvalidConfigurationException))]
        public async Task It_Should_Has_Error_with_No_Configuration_Match()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = MfeConfigurationNameMother.Create("current");
            var clusterConfigurationResult = MfeClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.For(new string[] { "previous" }), activeConfiguration);
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, activeConfiguration);
        }

        [TestMethod]
        public async Task It_Should_Find_Global_Configuration()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = MfeConfigurationNameMother.Random();
            var clusterConfigurationResult = MfeClusterConfigurationMother.Empty();//No Cluster configuration
            var globalConfigurationResult = MfeGlobalConfigurationMother.Random();
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);
            this.globalConfigurationRepository.Setup(g => g.Search(mfeId)).ReturnsAsync(globalConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, activeConfiguration);

            Assert.AreEqual<string>(response.VersionUrl, globalConfigurationResult.Configurations[activeConfiguration].Value);
            ;
            Assert.AreEqual(response.ConfigurationSource, $"global - {activeConfiguration.Value}");
        }

        [TestMethod]
        [ExpectedException(typeof(MfeClusterConfigurationDoesntExistsException))]
        public async Task It_Should_Has_Error_No_Configuration_at_All()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var activeConfiguration = MfeConfigurationNameMother.Random();
            var clusterConfigurationResult = MfeClusterConfigurationMother.Empty();//No Cluster configuration
            var globalConfigurationResult = MfeGlobalConfigurationMother.Empty();
            this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);
            this.globalConfigurationRepository.Setup(g => g.Search(mfeId)).ReturnsAsync(globalConfigurationResult);

            var response = await this.finder.Execute(clusterId, mfeId, activeConfiguration);

        }
    }
}
