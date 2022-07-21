using Versioning.Service.ClusterConfigurations.Update;
using Versioning.Domain.GlobalConfigurations;
using Moq;
using Shared.Domain.Bus.Event;
using Versioning.Domain.ClusterConfigurations;
using Versioning.Domain.Tests.ClusterConfigurations;
using Versioning.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain.ValueObjects;

namespace Versioning.Service.Tests.ClusterConfigurations.Update
{
    [TestClass]
    public class ClusterConfigurationUpdaterShould : ClusterConfigurationModuleUnitTestCase
    {
        private readonly ClusterConfigurationUpdater updater;
        private readonly Mock<IGlobalConfigurationRepository> globalConfigurationRepository;
        private readonly Mock<IEventBus> eventBus;
        public ClusterConfigurationUpdaterShould()
        {
            this.globalConfigurationRepository = new Mock<IGlobalConfigurationRepository>();
            this.eventBus = new Mock<IEventBus>();
            this.updater = new ClusterConfigurationUpdater(this.Repository.Object, this.eventBus.Object);
        }

        [TestMethod]
        public async Task It_Should_Update_ClusterConfiguration_With_One_Configuration()
        {
            var mfeId = MfeIdMother.Random();
            var clusterId = ClusterIdMother.Random();
            var configurationToChange = ConfigurationNameMother.Random();
            var clusterConfigurationResult = ClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.First(3), configurationToChange);
            this.Repository.Setup(r => r.SearchBatch(mfeId, It.IsAny<List<ClusterId>>()))
                .ReturnsAsync(new List<ClusterConfiguration>() {clusterConfigurationResult});

            await this.updater.Execute(mfeId, configurationToChange, new List<ClusterId>() { ClusterIdMother.Random() }, VersionUrlMother.Random(), true);

            //Assert.AreEqual<string>(response.VersionUrl, clusterConfigurationResult.Configurations[configurationToChange].Value);
            //Assert.AreEqual(response.ConfigurationSource, $"cluster - {configurationToChange.Value}");
        }

        //[TestMethod]
        //[ExpectedException(typeof(NoActiveClusterConfigurationExistsException))]
        //public async Task It_Should_Has_Error_with_Active_Empty_Configuration()
        //{
        //    var mfeId = MfeIdMother.Random();
        //    var clusterId = ClusterIdMother.Random();
        //    var activeConfiguration = ConfigurationNameMother.Create("");
        //    var clusterConfigurationResult = ClusterConfigurationMother.Create(mfeId, clusterId, ConfigurationListMother.Random(), activeConfiguration);
        //    this.Repository.Setup(r => r.Search(mfeId, clusterId)).ReturnsAsync(clusterConfigurationResult);

        //    var response = await this.updater.Execute(clusterId, mfeId, null);
        //}        
    }
}
