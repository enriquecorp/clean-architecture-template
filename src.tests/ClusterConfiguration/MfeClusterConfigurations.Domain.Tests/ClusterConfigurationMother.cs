using Versioning.Shared.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain;

namespace MfeClusterConfigurations.Domain.Tests
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
            return Create(MfeIdMother.Random(), ClusterIdMother.Random(), ConfigurationListMother.Random(), ConfigurationNameMother.Random());
        }

        public static ClusterConfiguration? Empty()
        {
            return null;
        }
    }
}
