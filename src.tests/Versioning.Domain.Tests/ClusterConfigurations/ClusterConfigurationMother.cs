using Versioning.Domain.ValueObjects;
using Versioning.Domain.ClusterConfigurations;
using Versioning.Domain.Constants;
using Versioning.Shared.Tests.Domain.ValueObjects;

namespace Versioning.Domain.Tests.ClusterConfigurations
{
    public class ClusterConfigurationMother
    {
        public static ClusterConfiguration Create(MfeId name, ClusterId id, ConfigurationList configurations, ConfigurationName activeConfiguration)
        {
            return new ClusterConfiguration(id, name, activeConfiguration, configurations);
        }

        //public static ClusterConfiguration FromRequest(CreateClusterConfigurationCommand request)
        //{
        //    return Create(MfeIdMother.Create(request.MfeId), ClusterIdMother.Create(request.ClusterId), ConfigurationListMother.Create(request.Configurations), ConfigurationNameMother.Create(request.ActiveConfiguration));
        //}

        public static ClusterConfiguration Random()
        {
            var configurationName = ConfigurationNameMother.Random();
            var configurationListMinSize = Configuration.SupportedConfigurations.IndexOf(configurationName.Value) + 1;
            return Create(MfeIdMother.Random(), ClusterIdMother.Random(), ConfigurationListMother.Random(configurationListMinSize), configurationName);
        }

        public static ClusterConfiguration? Empty()
        {
            return null;
        }
    }
}
