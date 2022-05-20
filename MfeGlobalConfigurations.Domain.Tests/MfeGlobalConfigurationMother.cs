using Versioning.Shared.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain;

namespace MfeGlobalConfigurations.Domain.Tests
{
    public class MfeGlobalConfigurationMother
    {
        public static MfeGlobalConfiguration Create(MfeId name, ConfigurationList configurations, MfeConfigurationName activeConfiguration)
        {
            return new MfeGlobalConfiguration(name, activeConfiguration, configurations);
        }

        //public static MfeClusterConfiguration FromRequest(CreateClusterConfigurationCommand request)
        //{
        //    return Create(MfeIdMother.Create(request.MfeId), ClusterIdMother.Create(request.ClusterId), ConfigurationListMother.Create(request.Configurations), MfeConfigurationNameMother.Create(request.ActiveConfiguration));
        //}

        public static MfeGlobalConfiguration Random()
        {
            return Create(MfeIdMother.Random(), ConfigurationListMother.Random(), MfeConfigurationNameMother.Random());
        }

        public static MfeGlobalConfiguration? Empty()
        {
            return null;
        }
    }
}
