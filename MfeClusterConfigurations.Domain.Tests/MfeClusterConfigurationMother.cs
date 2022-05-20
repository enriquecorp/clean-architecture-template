using Versioning.Shared.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain;

namespace MfeClusterConfigurations.Domain.Tests
{
    public class MfeClusterConfigurationMother
    {
        public static MfeClusterConfiguration Create(MfeId name, ClusterId id, ConfigurationList configurations, MfeConfigurationName activeConfiguration)
        {
            return new MfeClusterConfiguration(id, name, activeConfiguration, configurations);
        }

        //public static MfeClusterConfiguration FromRequest(CreateClusterConfigurationCommand request)
        //{
        //    return Create(MfeIdMother.Create(request.MfeId), ClusterIdMother.Create(request.ClusterId), ConfigurationListMother.Create(request.Configurations), MfeConfigurationNameMother.Create(request.ActiveConfiguration));
        //}

        public static MfeClusterConfiguration Random()
        {
            return Create(MfeIdMother.Random(), ClusterIdMother.Random(), ConfigurationListMother.Random(), MfeConfigurationNameMother.Random());
        }

        public static MfeClusterConfiguration? Empty()
        {
            return null;
        }
    }
}
