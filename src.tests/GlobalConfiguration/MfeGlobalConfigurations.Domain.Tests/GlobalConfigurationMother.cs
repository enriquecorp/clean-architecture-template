using Versioning.Shared.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain;

namespace MfeGlobalConfigurations.Domain.Tests
{
    public class GlobalConfigurationMother
    {
        public static GlobalConfiguration Create(MfeId name, ConfigurationList configurations, ConfigurationName activeConfiguration)
        {
            return new GlobalConfiguration(name, activeConfiguration, configurations);
        }

        //public static ClusterConfiguration FromRequest(CreateClusterConfigurationCommand request)
        //{
        //    return Create(MfeIdMother.Create(request.MfeId), ClusterIdMother.Create(request.ClusterId), ConfigurationListMother.Create(request.Configurations), ConfigurationNameMother.Create(request.ActiveConfiguration));
        //}

        public static GlobalConfiguration Random()
        {
            return Create(MfeIdMother.Random(), ConfigurationListMother.Random(), ConfigurationNameMother.Random());
        }

        public static GlobalConfiguration? Empty()
        {
            return null;
        }
    }
}
