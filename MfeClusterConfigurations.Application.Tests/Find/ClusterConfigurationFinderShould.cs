﻿using System.Threading.Tasks;
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
    }
}
